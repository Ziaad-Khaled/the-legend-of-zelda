using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //Health Variables
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite emptyHeart;

    //Abilities Variables
    [SerializeField]
    private Image BombImage;
    [SerializeField]
    private Image CryonisImage;
    [SerializeField]
    private Image StasisImage;

    [SerializeField]
    private Sprite BombActive;
    [SerializeField]
    private Sprite BombDeact;
    [SerializeField]
    private Sprite CryonisActive;
    [SerializeField]
    private Sprite CryonisDeact;
    [SerializeField]
    private Sprite CryonisNA;
    [SerializeField]
    private Sprite StasisActive;
    [SerializeField]
    private Sprite StasisDeact;
    [SerializeField]
    private Sprite StasisNA;

    //Attack Mode Variables
    [SerializeField]
    private Image weaponSelectionUI;
    [SerializeField]
    private Sprite noneSelected;
    [SerializeField]
    private Sprite meleeSelected;
    [SerializeField]
    private Sprite rangeSelected;

    //Takes in Link's health points and updates heart UI accordingly. Note: For < 0 sets to 0. For > 24 sets to 24.
    public void updateHealthUI(int HealthPoints)
    {
        if (HealthPoints <= 0 || HealthPoints >= 24)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = HealthPoints <= 0 ? emptyHeart : fullHeart;
            }
        }
        else
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < ((HealthPoints - 1) / 2))
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    if (i == ((HealthPoints - 1) / 2))
                    {
                        if (HealthPoints % 2 == 1)
                        {
                            hearts[i].sprite = halfHeart;
                        }
                        else
                        {
                            hearts[i].sprite = fullHeart;
                        }
                    }
                    else
                    {
                        hearts[i].sprite = emptyHeart;
                    }
                }
            }
        }
    }

    //Takes in 1,2, or 4 and updates the ability UI accordingly. If any other number then all are deact.
    public void updateAbilityUI(int abilityButton)
    {
        switch (abilityButton)
        {
            case -1:
                BombImage.sprite = BombActive;
                CryonisImage.sprite = CryonisNA;
                StasisImage.sprite = StasisNA;
                break;
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


    //To disable in shrine, input anything into the text field (ex: "none")
    public void updateWeaponUI(string weapon)
    {
        switch (weapon)
        {
            case "melee":
                weaponSelectionUI.sprite = meleeSelected;
                break;
            case "ranged":
                weaponSelectionUI.sprite = rangeSelected;
                break;
            default:
                weaponSelectionUI.sprite = noneSelected;
                break;
        }
    }
}
