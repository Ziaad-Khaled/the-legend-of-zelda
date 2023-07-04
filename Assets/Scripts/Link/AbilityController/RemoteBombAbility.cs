using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class RemoteBombAbility : AbilityState
{
    GameObject bombThrown;
    ParticleSystem explosionEffect;
    float radius = 10f;
    float force = 700f;
    GameObject plane;
    IceCubesController iceCubesController;
    GameObject bombInHand;
    Animator ThirdPersonAnimator;
    AudioClip explosionSound;
    private bool triggerEnetered = false;

    public RemoteBombAbility (GameObject bombThrown, GameObject bombInHand, ParticleSystem explosionEffect,
        GameObject plane, IceCubesController iceCubesController, Animator ThirdPersonAnimator, AudioClip explosionSound)
    {
        this.bombThrown = bombThrown;
        this.explosionEffect = explosionEffect;
        this.plane = plane;
        this.iceCubesController = iceCubesController;
        this.bombInHand = bombInHand;
        this.ThirdPersonAnimator = ThirdPersonAnimator;
        this.explosionSound = explosionSound;
    }

    public RemoteBombAbility(GameObject bombThrown, GameObject bombInHand, ParticleSystem explosionEffect,
          Animator ThirdPersonAnimator, AudioClip explosionSound)
    {
        this.bombThrown = bombThrown;
        this.explosionEffect = explosionEffect;
        this.bombInHand = bombInHand;
        this.ThirdPersonAnimator = ThirdPersonAnimator;
        this.explosionSound = explosionSound;

    }

    public override bool performAbility()
    {
        //do not perform ability if the player is in middle of throwing
        if (!bombThrown.activeSelf) return false;//do not perform ability unless the bombThrown is in the scene
        

        explosionEffect.transform.position = bombThrown.transform.position;
        explosionEffect.Play();
        bombThrown.SetActive(false);
        bombThrown.transform.position = bombInHand.transform.position;
        AudioManager.instance.PlaySFX(explosionSound);
        Collider[] colliders =  Physics.OverlapSphere(bombThrown.transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            if ((nearbyObject.CompareTag("0") || nearbyObject.CompareTag("1") || nearbyObject.CompareTag("2") || nearbyObject.CompareTag("3"))
                && nearbyObject.isTrigger)
            { 
                    GameObject controller = GameObject.FindGameObjectWithTag("controller");
                    controllerScript cs = controller.GetComponent<controllerScript>();
                    int[][] enemyGroupArray = cs.enemyGroupArray;
                    cs.enemyGroupArray[cs.currentEnemyGroupNo][Int32.Parse(nearbyObject.gameObject.tag)] -= 10;
                    int newHealth = cs.enemyGroupArray[cs.currentEnemyGroupNo][Int32.Parse(nearbyObject.gameObject.tag)];
                    if (newHealth < 0)
                        cs.enemyGroupArray[cs.currentEnemyGroupNo][Int32.Parse(nearbyObject.gameObject.tag)] = 0;
            }
            else if(nearbyObject.CompareTag("Boss"))
            {
                BossHealthController bossHealthConteroller =  nearbyObject.GetComponent<BossHealthController>();
                bossHealthConteroller.TakeDamage(10);
                bossHealthConteroller.PlayBossBombHit();
            }
            else
            {
                //if the collided object is anything else
                if (nearbyObject.CompareTag("Player"))//do not apply force on the player
                    continue;

                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null && !nearbyObject.gameObject.CompareTag("0") && !nearbyObject.gameObject.CompareTag("1")
                    && !nearbyObject.gameObject.CompareTag("2") && !nearbyObject.gameObject.CompareTag("3"))
                {
                    rb.AddExplosionForce(force, bombThrown.transform.position, radius);
                }
            }
        }

        return true;



    }
    public override void setAbilityActive()
    {
        //unperform stasis, if any
        if (plane != null)
        {
            PlaneUpDown planeupdown = plane.GetComponent<PlaneUpDown>();
            planeupdown.UnfreezeHelper();
        }

        //unperform cryonis, if any
        if(iceCubesController!=null)
            iceCubesController.unperformCryonis();


        // avoid any reload.
        bombInHand.SetActive(true);
        ThirdPersonAnimator.SetTrigger("Throw");
        BombController bombController = bombInHand.GetComponent<BombController>();
        bombController.InactivateBombInHand();
    }
}
