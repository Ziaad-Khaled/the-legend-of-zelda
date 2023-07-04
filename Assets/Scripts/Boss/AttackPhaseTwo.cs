using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhaseTwo : AttackScript
{
    // Start is called before the first frame update
    public GameObject projectile;
    public float projectileSpeed = 100f;
    public GameObject throwingHand;
    // have a reference to the neck of the boss to know where the boss is looking at
    public GameObject Neck;
    private bool throwFireBall;
    Animator anim;
    bool attacking;
    float attackTime;
    void Start()
    {
        anim = GetComponent<Animator>();
        attacking = false;
        UpdateAnimClipTimes();
    }

    // Update is called once per frame
   
    public override void Attack()
    {
        attacking = true;
        anim.SetTrigger("attack2");
        //Trigger the animation here
        //Trigger the start animation events here
       // StartCoroutine(Attacking());
    }
    public void SpawnBigBall()
    {
        Debug.Log("tHrowing la bomba");
        GameObject fireball = Instantiate(projectile, new Vector3(throwingHand.transform.position.x, throwingHand.transform.position.y, throwingHand.transform.position.z), Quaternion.identity, null) as GameObject;
        fireball.transform.forward = -transform.forward;
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        //rb.velocity = this.transform.forward * projectileSpeed;
        Vector3 ballDirection = transform.forward;
        rb.AddForce(ballDirection * projectileSpeed, ForceMode.Acceleration);
    }
   
    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            
            switch (clip.name)
            {
                case "Attack":
                    attackTime = clip.length;
                    break;
                //case "Damage":
                //    damageTime = clip.length;
                //    break;
                //case "Dead":
                //    deathTime = clip.length;
                //    break;
                //case "Idle":
                //    idleTime = clip.length;
                //    break;
            }
        }
    }
}
