using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuUI;
    private float currentTimescale = 1.0f;
    [SerializeField]
    private Button resumeButton;
    public GameObject player;
    private Animator animator;
    private CombatController combatController;

    void Start()
    {
        animator = player.GetComponent<Animator>();
        combatController = player.GetComponent<CombatController>();
    }

    public void togglePause()
    {
        if (SceneChanger.isPaused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }

    private void pause()
    {
        PauseMenuUI.SetActive(true);
        SceneChanger.instance.setPaused(true);
        resumeButton.Select();
        currentTimescale = Time.timeScale;
        Time.timeScale = 0.0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //adjusting Audio
        AudioManager.instance.onPauseAudioHandler();
        animator.enabled = false;
        combatController.enabled = false;
    }

    private void resume()
    {
        PauseMenuUI.SetActive(false);
        SceneChanger.instance.setPaused(false);
        Time.timeScale = currentTimescale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioManager.instance.onResumeAudioHandler();
        StartCoroutine(ToggleAnimator());
    }

    public void onClickResume()
    {
        togglePause();
    }

    public void onClickRestart()
    {
        resume();
        SceneChanger.instance.reloadScene();
    }
    public void onClickQuit()
    {
        SceneChanger.instance.setPaused(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.onResumeAudioHandler();
        Time.timeScale = 1.0f;
        SceneChanger.instance.changeScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            togglePause();
        }
    }

    IEnumerator ToggleAnimator()
  {
    yield return new WaitForSeconds(0.4f);
    // Activate the animator
    animator.enabled = !animator.enabled;
    combatController.enabled = !combatController.enabled;
  }
}
