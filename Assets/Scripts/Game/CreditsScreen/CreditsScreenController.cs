using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreenController : MonoBehaviour
{
    [SerializeField] private AudioClip creditsClip;
    private void Awake()
    {
        AudioManager.instance.changeMusic(creditsClip);
    }
    public void onClickMainMenu()
    {
        SceneChanger.instance.changeScene("MainMenu");
    }
}
