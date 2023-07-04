using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowMotionController : MonoBehaviour
{
    public Rigidbody arrowRb;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Boss")
        {
            scale = 3;
        }
        else
        {
            scale = 1;
        }

        transform.Rotate(Vector3.forward, -90);
        transform.Rotate(Vector3.right, 90);
        transform.position += new Vector3(-0.2f*scale,0.5f * scale,0f * scale);
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootArrow()
    {
        arrowRb.AddForce(transform.forward* 50f, ForceMode.Impulse);
    }
}
