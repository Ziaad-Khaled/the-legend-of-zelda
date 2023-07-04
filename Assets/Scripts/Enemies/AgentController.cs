using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Threading;

public class AgentController : MonoBehaviour
{
    // target is link and agent is the enemy

    public GameObject player;
    public GameObject controller;

    private NavMeshAgent agent;

    Animator a;
    private Rigidbody rb;
    private controllerScript cs;
    private int[][] enemyGroupArray;
    bool attacking;
    float attackTimer;
   
    Transform target;

    int attackType = 2;
    private HealthController healthController;
    int enemyIndex;

    public bool playerSeen = false;
    bool startDying = false;
    bool canAttack = true;
    public bool dead = false;
    private bool triggerEntered=false;




    void Start()
    {
        target = player.transform;
        a = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        cs = controller.GetComponent<controllerScript>();
        enemyGroupArray = cs.enemyGroupArray;
        healthController = player.GetComponent<HealthController>();
        enemyIndex = Int32.Parse(this.gameObject.tag);
    }

  
    void Update()
    {
//Enemy Health Less than 0
        if (cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] == 0 && startDying == false)
        {
            startDying=true;

            playDeath();
            rb.isKinematic = true;
            agent.isStopped = true;
            agent.ResetPath();

            a.SetTrigger("Dying");
            StartCoroutine(dieAnimationDelay());

        
        }

        //Enemy Health More than 0
        else
        {

            if (Vector3.Distance(transform.position, target.position) > 25 && playerSeen == false)
            {
                rb.isKinematic = false;
                agent.isStopped = true;
                a.SetFloat("Blend", 0);


            }

            if (Vector3.Distance(transform.position, target.position) < 25 && Vector3.Distance(transform.position, target.position) > 1 && startDying == false)
            {

                playerSeen = true;
                rb.isKinematic = true;
                agent.isStopped = false;
                a.SetFloat("Blend", 1);
                agent.SetDestination(target.position);

            }

            if (Vector3.Distance(transform.position, target.position) < 1)
            {

                rb.isKinematic = true;
                agent.isStopped = true;

                if (!startDying)
                {
                    if (attacking == false)
                    {
                        System.Random rnd = new System.Random();
                        attackType = rnd.Next(2, 10);
                        attacking = true;
                    }
                    else
                    {
                        attackTimer += Time.deltaTime;
                        if (attackTimer > 2)
                        {
                            if (attackType == 3) //vertical attack
                            {

                                if (transform.name == "enemy0" || transform.name == "enemy1")
                                {
                                    performDamage(3);
                                }
                                else
                                {
                                    performDamage(4);
                                }

                            }
                            else if (attackType == 2) //horizontal attack
                            {
                                if (transform.name == "enemy0" || transform.name == "enemy1")
                                {
                                    performDamage(1);
                                }
                                else
                                {
                                    performDamage(2);
                                }

                            }
                            attackTimer = 0;
                            attacking = false;
                        }
                    }
                    if (attackType > 3)
                    {
                        a.SetFloat("Blend", 0);
                    }
                    else
                    {
                        a.SetFloat("Blend", attackType);
                    }

                   
                }
            }

            if (Vector3.Distance(transform.position, target.position) > 25 && playerSeen == true)
            {

                rb.isKinematic = false;
                agent.isStopped = false;
                agent.SetDestination(target.position);


            }
        }
    }

  
    void performDamage(int damage)
    {
        healthController.PerformDamage(damage);
    }


    





    IEnumerator dieAnimationDelay()
    {       
        yield return new WaitForSeconds(4);
        dead = true;       
    }

    IEnumerator triggerDelay()
    {
        yield return new WaitForSeconds(0.2f);
        triggerEntered = false;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PlayerSword") && !triggerEntered){
              triggerEntered = true;
              StartCoroutine(triggerDelay());

              cs.playSound("sword");
                 print("entered sword collider");
                 a.SetTrigger("Hit");
                //detect which zone and which enemy & deduct health accordingly
            
                
                cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] -= 10;
            //update health in other script

            if (cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] < 0)
            {
                cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] = 0;
                triggerEntered = false;
            }
                    
            
            

        }




        if (other.gameObject.CompareTag("PlayerArrow"))
        {
            cs.playSound("arrow");
            a.SetTrigger("Hit");
       
            cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] -= 5;

            if (cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] < 0)
                cs.enemyGroupArray[cs.currentEnemyGroupNo][enemyIndex] = 0;
            //play arrow hit sound
        }
    }
    public void playrun()
    {
        cs.playRunSound();
    }
    public void playDeath()
    {
        if (cs.currentEnemyGroupNo!=0) // don't play in first zone
        cs.playDyingSound();
    }

    public void respawn()
    {
    playerSeen = false;
    startDying = false;
    canAttack = true;
    dead = false;
    rb.isKinematic = false ;
    agent.isStopped = false;

    } 
}














