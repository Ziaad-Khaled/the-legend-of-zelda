// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class BossBehaviour : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    NavMeshAgent agent;
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;


    // referencing other scripts
    public  AttackScript attacker;
    public LookAt lookAt;
    //modulating agent behaviour
    public float startWaitTime = 1.0f;                 //  Wait time of every action
    public float timeToRotate = 0.1f;                  //  Wait time when the enemy detect near the player without seeing
    public float speedWalk = 6;                     //  Walking speed, speed in the nav mesh agent
    public float speedRun = 9;
    

    //Detecting Link
    public LayerMask obstacleMask;
    public LayerMask playerMask;
    public float viewRadius = 100f;                   //  Radius of the enemy view
    public float viewAngle = 180;
    public Transform neck;


    Vector3 playerLastPosition = Vector3.zero;      //  Last position of the player when was near the enemy
    Vector3 m_PlayerPosition;
    float m_WaitTime;                               //  Variable of the wait time that makes the delay
    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
    bool m_playerInRange;                           //  If the player is in range of vision, state of chasing
    bool m_PlayerNear;                              //  If the player is near, state of hearing
    bool m_IsPatrol;                                //  If the enemy is patrol, state of patroling
    bool m_CaughtPlayer;

    public AudioClip bossLevelMusicPhaseOne;
    public AudioClip bossLevelMusicPhaseTwo;
    public AudioClip footStepSFX;
    public AudioClip SmallFireBallSFX;
    public AudioClip BigFireBallSFX;
    public AudioClip BossRoarSFX;


    private Collider[] childrenColliders;


    void Start()
    {
       agent = GetComponent<NavMeshAgent>();


        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        //agent.autoBraking = false;
        anim = GetComponent<Animator>();
        agent.updatePosition = false;
        agent.autoBraking = false;



         attacker = GetComponent<AttackPhaseOne>();
         lookAt = GetComponent<LookAt>();



        // adding all colliders to an array, but our collider will be added to !
        childrenColliders = GetComponentsInChildren<Collider>();


        foreach (Collider col in childrenColliders)
        {
            // checking if it is our collider, then skip it, 
            if (col != GetComponent<Collider>())
            {
                // if it is not our collider then ignore collision between our collider and childs collider
                Physics.IgnoreCollision(col, GetComponent<Collider>());
                col.gameObject.tag = "BossBodyPart";
            }
        }

        GotoNextPoint();
    }
    private void Awake()
    {
        AudioManager.instance.changeMusic(bossLevelMusicPhaseOne);
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.

        /// set animation parameters
        agent.destination = points[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
       


    }


    void Update()
    {
        
        moveCharacterAndAnimate();


        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();


        if (lookAt)
            lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;


       
        if (m_IsPatrol)
        {
            Patroling();
        }
        else
        {
            Chasing();
        }


    }
    private void FixedUpdate()
    {
        EnviromentView();
    }
    private void Patroling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        LookAt lookAt = GetComponent<LookAt>();
        if (lookAt)
            lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;
    }
    public void EnterPhaseTwo()
    {
        AudioManager.instance.changeMusic(bossLevelMusicPhaseTwo);
        attacker = GetComponent<AttackPhaseTwo>();
    }


    private void Chasing()
    {
        //  The enemy is chasing the player
        m_PlayerNear = false;                       //  Set false that hte player is near beacause the enemy already sees the player
        playerLastPosition = Vector3.zero;          //  Reset the player near position
         if(m_playerInRange)
        {
            if (Vector3.Distance(m_PlayerPosition, transform.position) <= 10f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(m_PlayerPosition - transform.position), 1f * Time.deltaTime);

                attacker.Attack();
            }
            else
            {
                agent.SetDestination(m_PlayerPosition);
            }
        }
        else
        {
            if (m_WaitTime <= 0)
            {
                
                GotoNextPoint();
                m_IsPatrol = true;

            }
            else
            {
                m_WaitTime -= Time.deltaTime;
            }
           
        }
      
        
    }
    
  

    void moveCharacterAndAnimate()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        // bool shouldMove = velocity.magnitude > 0.05f && agent.remainingDistance > agent.radius;
        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", shouldMove);
        anim.SetFloat("valx", velocity.x);
        anim.SetFloat("valy", velocity.y);

        //GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
        //code for agent follows character (animation)
        if (worldDeltaPosition.magnitude > agent.radius)
            agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;


        //code for character follows animation
        //if (worldDeltaPosition.magnitude > agent.radius)
        //    transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;

    }



    void OnAnimatorMove()
    {
        // Update position based on animation movement using navigation surface height
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(player.position, transform.position);
                
                Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask);
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, dirToPlayer, out hit, dstToPlayer, obstacleMask))

                {
                    
                    if (hit.collider.CompareTag("obstacle"))
                    {
                       
                    }
                }
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {

                    
                    m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                    m_IsPatrol = false;                 //  Change the state to chasing the player
                }
                else
                {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                   
                    m_playerInRange = false;

                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will no chase
                 * */

                m_playerInRange = false;                //  Change the sate of chasing
            }
            if (m_playerInRange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */

                m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
            }
            else
            {
                //m_WaitTime = startWaitTime;
            }

            
        }
    }
    public void PlayFootStepSound()
    {
        AudioManager.instance.PlaySFX(footStepSFX);
    }
    public void PlaySmallFireBallSound()
    {
        AudioManager.instance.PlaySFX(SmallFireBallSFX);
    }
    public void PlayBigFireBallSound()
    {
        AudioManager.instance.PlaySFX(BigFireBallSFX);
    }
    public void PlayBossRoarSound()
    {
        AudioManager.instance.PlaySFX(BossRoarSFX);
    }
}