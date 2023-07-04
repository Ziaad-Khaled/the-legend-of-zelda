using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public BossHealthController health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("PlayerSword"))
        { 
            health.TakeDamage(5);
            Debug.Log("Damage taken. HP: " + health.health);
        }
    }
  
}
