using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIre_ball_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explosionVFX;
    private HealthController linkHealthController;
    public BossHealthController bossHealthController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (!(other.CompareTag("Boss") || other.CompareTag("BossBodyPart") || other.CompareTag("Fireball")))
        {
            if (other.CompareTag("Player"))
            {
                if (linkHealthController == null)
                {
                    linkHealthController = other.GetComponent<HealthController>();
                }
                if (bossHealthController.IsPhaseTwo())
                {
                    linkHealthController.PerformDamage(5);
                }
                else
                {
                    linkHealthController.PerformDamage(2);
                }
                
            }
            explosionVFX.transform.position = this.transform.position;
            explosionVFX.Play();
            Destroy(gameObject);
               
            
           
        }
       

    }
}
