using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;
    private string previousSceneName = "MainMenu";
    public static bool isPaused = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void reloadScene()
    {
        Scene Currentscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Currentscene.name);
    }

    public void changeSceneGameover()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        AudioManager.instance.stopMainMusic();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //StartCoroutine(GameOverCoroutine());
        SceneManager.LoadScene("GameOver");
    }
    
    public void reloadFromGameover()
    {
        SceneManager.LoadScene(previousSceneName);
    }

    public void setPaused(bool value)
    {
        isPaused = value;
    }

    IEnumerator GameOverCoroutine()
  {
    yield return new WaitForSeconds(3f);
    // Activate the animator
    SceneManager.LoadScene("GameOver");
  }
}
