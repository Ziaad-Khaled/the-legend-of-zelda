using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoiceController : MonoBehaviour
{

    public Image weaponSelectionUI;
    public Sprite noneSelected;
    public Sprite meleeSelected;
    public Sprite rangeSelected;
    public string weaponChoice; //only for testing
    void Update()
    {
        updateWeaponUI(weaponChoice);
    }

    //To disable in shrine, input anything into the text field (ex: "none")
    public void updateWeaponUI(string weapon)
    {
        switch(weapon){
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
