using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] MainButtons;
    public GameObject LevelSelectScreen;
    public GameObject OptionsMenu;
    public GameObject TeamCredits;
    public GameObject AssetCredits;
    public Button OptionsButton;
    public Button OverworldButton;
    public Button NewGameButton;
    public Button SelectLevel;
    public Button TeamCreditsButton;
    public Button TeamCreditsBack;
    public Button AssetCreditsButton;
    public Button AssetCreditsBack;

    public AudioClip mainMenuMusic;
    private void Awake()
    {
        AudioManager.instance.changeMusic(mainMenuMusic);
        NewGameButton.Select();
    }
    //On Click Events
    public void onSelectLevelClick()
    {
        foreach (GameObject button in MainButtons){
            button.SetActive(false);
        }
        LevelSelectScreen.SetActive(true);
        OverworldButton.Select();
    }

    public void onOverworldClick()
    {
        SceneChanger.instance.changeScene("Scene1");
    }

    public void onShrineClick()
    {
        SceneChanger.instance.changeScene("Shrine");
    }

    public void onBossClick()
    {
        SceneChanger.instance.changeScene("Boss");
    }

    public void onBackClick()
    {
        foreach (GameObject button in MainButtons)
        {
            button.SetActive(true);
        }
        SelectLevel.Select();
        LevelSelectScreen.SetActive(false);
    } 

    public void onQuitClick()
    {
        Application.Quit();
    }

    public void onOptionsClick()
    {
        foreach (GameObject button in MainButtons)
        {
            button.SetActive(false);
        }
        OptionsMenu.SetActive(true);
        TeamCreditsButton.Select();
    }

    public void onTeamClick()
    {
        OptionsMenu.SetActive(false);
        TeamCredits.SetActive(true);
        TeamCreditsBack.Select();
    }

    public void onAssetsClick()
    {
        OptionsMenu.SetActive(false);
        AssetCredits.SetActive(true);
        AssetCreditsBack.Select();
    }

    public void onBackFromOptions()
    {
        foreach (GameObject button in MainButtons)
        {
            button.SetActive(true);
        }
        OptionsButton.Select();
        OptionsMenu.SetActive(false);
    }

    public void onBackFromTeam()
    {
        OptionsMenu.SetActive(true);
        TeamCreditsButton.Select();
        TeamCredits.SetActive(false);
    }

    public void onBackFromAssets()
    {
        OptionsMenu.SetActive(true);
        AssetCreditsButton.Select();
        AssetCredits.SetActive(false);
    }
}
