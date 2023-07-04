using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerScript : MonoBehaviour
{
    public AudioClip swordSound;
    public AudioClip arrowSound;

    public GameObject player;
    public GameObject enemies;
    //terrain width =1000 --> x range 0->1000
    //terrain depth =1000 -> z range 0->1000
    // 25 zones (5 rows x 5 columns)

    int zoneX;
    int zoneZ;

    int currentX;
    int currentZ;

    int currentZoneCenterX;
    int currentZoneCenterZ;

    GameObject enemy0;
    GameObject enemy1;
    GameObject enemy2;
    GameObject enemy3;

    AgentController enemy0Controller;
    AgentController enemy1Controller;
    AgentController enemy2Controller;
    AgentController enemy3Controller;

    public GameObject healthBar0; //healthbar enemy0
    public GameObject healthBar1; //healthbar enemy1
    public GameObject healthBar2; //healthbar enemy2
    public GameObject healthBar3; //healthbar enemy3

    int[] enemyGroup = new int[4]{ 20, 20, 30, 30 };

    public int[] currentEnemyGroup;
    public int[][] enemyGroupArray = new int[25][];
    public int currentEnemyGroupNo;
    public AudioClip run;
    public AudioClip die;
    public AudioClip sceneSound;
    //public AudioSource run;
    //public AudioSource die;
    public bool runningSound=false;
    public bool canPlayRun=true;
    public GameObject HUDGameObject;
    private HUDController HUD;
    private void Awake()
    {
        AudioManager.instance.changeMusic(sceneSound);
        HUD = HUDGameObject.GetComponent<HUDController>();
        HUD.updateAbilityUI(-1);
    }
    void Start()
    {
       
        for (int i = 0; i < enemyGroupArray.Length; i++)
       {
            enemyGroupArray[i] = new int[4] { 20, 20, 30, 30 };
        }

        enemyGroupArray[0] = new int[4] { 0, 0, 0, 0 };

        enemy0 = enemies.transform.GetChild(0).gameObject;
        enemy1 = enemies.transform.GetChild(1).gameObject;
        enemy2 = enemies.transform.GetChild(2).gameObject;
        enemy3 = enemies.transform.GetChild(3).gameObject;

        enemy0Controller = enemy0.GetComponent<AgentController>();
        enemy1Controller = enemy1.GetComponent<AgentController>();
        enemy2Controller = enemy2.GetComponent<AgentController>();
        enemy3Controller = enemy3.GetComponent<AgentController>();

        Vector3 playerPos = player.transform.position;
        zoneX = (int)playerPos.x / 200;
        zoneZ = (int)playerPos.z / 200;
        updateZone();
    }
    


  
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        zoneX = (int)playerPos.x / 200;
        zoneZ = (int)playerPos.z / 200;


        enemy0Controller = enemy0.GetComponent<AgentController>();
        enemy1Controller = enemy1.GetComponent<AgentController>();
        enemy2Controller = enemy2.GetComponent<AgentController>();
        enemy3Controller = enemy3.GetComponent<AgentController>();

        //Current Enemies Health

        int currentEnemy0Health = currentEnemyGroup[0];
        int currentEnemy1Health = currentEnemyGroup[1];
        int currentEnemy2Health = currentEnemyGroup[2];
        int currentEnemy3Health = currentEnemyGroup[3];

        

        //Health Bars Update
        float enemy0healthPercent = (float)currentEnemy0Health / 20;
        float enemy1healthPercent = (float)currentEnemy1Health / 20;
        float enemy2healthPercent = (float)currentEnemy2Health / 30;
        float enemy3healthPercent = (float)currentEnemy3Health / 30;

        healthBar0.transform.localScale = new Vector3(enemy0healthPercent, 0.1f, 0.01f);
        healthBar1.transform.localScale = new Vector3(enemy1healthPercent, 0.1f, 0.01f);
        healthBar2.transform.localScale = new Vector3(enemy2healthPercent, 0.1f, 0.01f);
        healthBar3.transform.localScale = new Vector3(enemy3healthPercent, 0.1f, 0.01f);

        if (currentEnemyGroupNo == 0)
        {
            enemy0.SetActive(false);
            enemy1.SetActive(false);
            enemy2.SetActive(false);
            enemy3.SetActive(false);

        }


        //Disable Enemies with health 0

        if (enemy0Controller.dead== true)
        {
            enemy0.SetActive(false);
        }
        if (enemy1Controller.dead == true)
        {
            enemy1.SetActive(false);
        }
        if (enemy2Controller.dead == true)
        {
            enemy2.SetActive(false);
        }
        if (enemy3Controller.dead == true)
        {
            enemy3.SetActive(false);
        }




        //Update Zone & Enemies Location (only if current enemies are killed)
        if (zoneX != currentX || zoneZ != currentZ )
        {
           

            int totalhealth = currentEnemy0Health + currentEnemy1Health + currentEnemy2Health + currentEnemy3Health;

            if (((enemy0Controller.playerSeen == false) && (enemy1Controller.playerSeen == false) && (enemy2Controller.playerSeen == false) &&
            (enemy3Controller.playerSeen == false))   ) {

                updateZone();
            }
            if (totalhealth <= 0) {
                updateZone();
            }
            

        }


    }
    void updateZone()
    {
        currentX = zoneX;
        currentZ = zoneZ;
        

        currentEnemyGroupNo = currentX + (currentZ * 5);
        currentEnemyGroup = enemyGroupArray[currentEnemyGroupNo];
        

        int minX = currentX * 200;
        int maxX = (currentX + 1) * 200;

        int minZ = currentZ * 200;
        int maxZ = (currentZ + 1) * 200;

        currentZoneCenterX = (minX + maxX) / 2;
        currentZoneCenterZ = (minZ + maxZ) / 2;

       

        updateEnemiesLocation();

    }
    void updateEnemiesLocation()
    {

        Vector3 originalPostion = enemies.transform.localPosition;

        enemies.transform.position = new Vector3(currentZoneCenterX, 0, currentZoneCenterZ);

        enemy0.transform.localPosition = new Vector3(0, 0, 0);
        enemy1.transform.localPosition = new Vector3(2, 0, 4);
        enemy2.transform.localPosition = new Vector3(4, 0, 0);
        enemy3.transform.localPosition = new Vector3(6, 0, 4);


        enemy0.SetActive(true);
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);

        //reset enemies in new zone (reset variables)
        enemy0Controller.respawn();
        enemy1Controller.respawn();
        enemy2Controller.respawn();
        enemy3Controller.respawn();






        int currentEnemy0Health = currentEnemyGroup[0];
        int currentEnemy1Health = currentEnemyGroup[1];
        int currentEnemy2Health = currentEnemyGroup[2];
        int currentEnemy3Health = currentEnemyGroup[3];



        if (currentEnemy0Health <= 0)
        {
            enemy0.SetActive(false);
        }
        if (currentEnemy1Health <= 0)
        {
            enemy1.SetActive(false);
        }
        if (currentEnemy2Health <= 0)
        {
            enemy2.SetActive(false);
        }
        if (currentEnemy3Health <= 0)
        {
            enemy3.SetActive(false);
        }

    }

    public void playSound(string clip)
    {
        if (clip=="sword")
        AudioManager.instance.PlaySFX(swordSound);
        if (clip == "arrow")
            AudioManager.instance.PlaySFX(arrowSound);


    }
    public void playRunSound()
    {
         

            AudioManager.instance.PlaySFX(run);
            
        
        
    }
    public void playDyingSound()
    {
        AudioManager.instance.PlaySFX(die);
        
    }
}
