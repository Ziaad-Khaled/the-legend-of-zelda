using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{
    // Start is called before the first frame update
    int MaxHealth = 200;
    [HideInInspector]
    public int health;
    Ragdoll ragdoll;

    // visualizing damage
    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer=0;
    Color original_mesh_color;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public bool EnteredPhaseTwo;
    Animator anim;

    // Hud controller
    public GameObject hudController;
    BossHUDController hUDManagerController;

    ShieldController shieldController;
    GameObject shield;

    //SFX
    public AudioClip bossDeathSound;
    public AudioClip bossHitSword;
    public AudioClip bossHitArrow;
    public AudioClip bossHitBomb;
    void Start()
    {
        health = MaxHealth;
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        original_mesh_color = skinnedMeshRenderer.material.color;

        //var rigidBodies = GetComponentsInChildren<Rigidbody>();
        //foreach(var rigidBody in rigidBodies)
        //{
        //    HitBox Hit = rigidBody.gameObject.AddComponent<HitBox>();
        //    Hit.health = this;
        shield = transform.GetChild(2).gameObject;
        //}


        EnteredPhaseTwo = false;
        anim = GetComponent<Animator>();

       
        


    }
    private void Awake()
    {
        hUDManagerController = hudController.GetComponent<BossHUDController>();
        hUDManagerController.setBossHealth(200);
        hUDManagerController.updateAbilityUI(-1);
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer <= 0)
        {
            blinkTimer = 0;
        }
        //insure ration between time and duration is always between 0 and 1
        float lerp = Math.Clamp(blinkTimer / blinkDuration, 0, 1);
        
        //setting blink intensity according to timer
        float intensity = lerp * blinkIntensity;
        skinnedMeshRenderer.material.color = original_mesh_color + Color.white * intensity;
    }
    public void TakeDamage(int amount)
    {
        if (shield.activeSelf)
            return;
        int damage_amount;
        if (EnteredPhaseTwo)
        {
            damage_amount = amount * 2;
        }
        else
        {
            damage_amount = amount;
        }

        
        health -= damage_amount;
        if(health <= 0)
        {
            health = 0;
            DisableShield();
            Die();
        }
        else if (health <= 150) {
            if (!EnteredPhaseTwo)
            {
                //make sure that we have instantiated shield. This is to overcome needing to define the order of which scripts start
                //shield = shieldController.shield;
                
               
                anim.SetTrigger("PhaseTwo");
                anim.SetBool("IsPhaseTwo", true);
                BossBehaviour bossBehaviour = GetComponent<BossBehaviour>();
                bossBehaviour.EnterPhaseTwo();
                EnteredPhaseTwo = true;
               

            }
           

        }
        blinkTimer = blinkDuration;
        
        hUDManagerController.setBossHealth(health);
       
    }

    private void Die()
    {
        AudioManager.instance.PlaySFX(bossDeathSound);
        anim.SetTrigger("Death");
        
    }
    public void ActivateRagdoll()
    {
        ragdoll.ActivateRagdoll();
        
    }
    public void TransitionToEndCredit()
    {
        SceneChanger.instance.changeScene("Credits");
    }

    
    private void OnTriggerEnter(Collider col)
    {
        if (shield.activeSelf == false)
        {
            if (col.gameObject.CompareTag("PlayerSword"))
            {
                PlayBossSwordHit();
                TakeDamage(10);
                
            }
            else if (col.gameObject.CompareTag("PlayerArrow"))
            {
                PlayBossArrowHit();
                TakeDamage(5);
                
            }
        }
    }
    public void EnableShield()
    {
        shield.SetActive(true);
    }
    public void DisableShield()
    {
        shield.SetActive(false);
    }
    public bool IsPhaseTwo()
    {
        return EnteredPhaseTwo;
    }
    public void PlayBossSwordHit()
    {
        AudioManager.instance.PlaySFX(bossHitSword);
    }
    public void PlayBossArrowHit()
    {
        AudioManager.instance.PlaySFX(bossHitArrow);
    }
    public void PlayBossBombHit()
    {
        AudioManager.instance.PlaySFX(bossHitBomb);
    }
   
}
