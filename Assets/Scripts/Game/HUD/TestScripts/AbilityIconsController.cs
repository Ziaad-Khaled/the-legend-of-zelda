using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconsController : MonoBehaviour
{
    public Image BombImage;
    public Image CryonisImage;
    public Image StasisImage;

    public Sprite BombActive;
    public Sprite BombDeact;
    public Sprite CryonisActive;
    public Sprite CryonisDeact;
    public Sprite StasisActive;
    public Sprite StasisDeact;

    public int ability;

    void Update()
    {
        updateAbilityUI(ability);//TODO: remove in integration
    }

    //Takes in 1,2, or 4 and updates the ability UI accordingly. If any other number then all are deact.
    public void updateAbilityUI(int abilityButton)
    {
        ability = abilityButton;
        switch (abilityButton)
        {
            case 1:
                BombImage.sprite = BombActive;
                CryonisImage.sprite = CryonisDeact;
                StasisImage.sprite = StasisDeact;
                break;
            case 2:
                BombImage.sprite = BombDeact;
                CryonisImage.sprite = CryonisActive;
                StasisImage.sprite = StasisDeact;
                break;
            case 4:
                BombImage.sprite = BombDeact;
                CryonisImage.sprite = CryonisDeact;
                StasisImage.sprite = StasisActive;
                break;
            default:
                BombImage.sprite = BombDeact;
                CryonisImage.sprite = CryonisDeact;
                StasisImage.sprite = StasisDeact;
                break;
        }
    }
}
