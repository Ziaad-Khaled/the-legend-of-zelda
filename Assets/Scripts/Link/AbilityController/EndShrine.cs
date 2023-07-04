using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndShrine : MonoBehaviour
{
    public AudioClip shrineClip;
    public GameObject endShrine;


    void Awake()
    {
        AudioManager.instance.changeMusic(shrineClip);
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.z >= endShrine.transform.position.z)
            SceneChanger.instance.changeScene("Boss");

    }
}
