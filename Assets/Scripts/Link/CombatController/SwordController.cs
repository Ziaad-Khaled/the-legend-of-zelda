using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerSword;
    private BoxCollider bc;
    void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
        bc = playerSword.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateSword()
    {
        bc.enabled = true;
    }
    public void DeactivateSword()
    {
        bc.enabled = false;
    }
}
