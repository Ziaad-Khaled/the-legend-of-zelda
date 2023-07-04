using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombController : MonoBehaviour
{
    public GameObject bombThrown;
    public GameObject player;



    public void InactivateBombInHand()
    {
        Invoke("InactivateBombInHandHelper", 0.4f);

    }

    void InactivateBombInHandHelper()
    {
        gameObject.SetActive(false);
        bombThrown.transform.position = gameObject.transform.position;
        bombThrown.SetActive(true);
        Rigidbody bombThrownRB = bombThrown.GetComponent<Rigidbody>();
        bombThrownRB.velocity = Vector3.zero;
        bombThrownRB.AddForce(player.gameObject.transform.forward * 10f, ForceMode.VelocityChange);

    }

}
