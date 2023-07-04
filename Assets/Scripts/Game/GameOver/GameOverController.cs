using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public AudioClip gameoverMenuMusic;
    private void Awake()
    {
        AudioManager.instance.PlayMusicOneTime(gameoverMenuMusic);
    }
    public void onClickQuit()
    {
        SceneChanger.instance.changeScene("MainMenu");
    }

    public void onClickRestart()
    {
        SceneChanger.instance.reloadFromGameover();
    }
}
