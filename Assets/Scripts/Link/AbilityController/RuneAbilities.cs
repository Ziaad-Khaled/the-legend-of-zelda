using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class RuneAbilities : MonoBehaviour
{
    public TMP_Text ability_text;
    
    AbilityState abilityState;
    AbilityState bombAbility;
    AbilityState stasisAbility;
    AbilityState cryonisAbility;
    public GameObject plane;
    public GameObject[] iceCubes;
    public GameObject bombInHand;
    public GameObject bombThrown;
    public Animator ThirdPersonAnimator;
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    public GameObject HUD;
    HUDController HUDController;

    // Start is called before the first frame update
    void Start()
    {
        bombAbility = new RemoteBombAbility(bombThrown, bombInHand, explosionEffect, plane,
            gameObject.GetComponent<IceCubesController>(), ThirdPersonAnimator, explosionSound);
        stasisAbility = new StasisAbility(plane, gameObject.GetComponent<IceCubesController>(), bombThrown);
        cryonisAbility = new CryonisAbility(this.gameObject, iceCubes, plane, bombThrown);

        HUDController = HUD.GetComponent<HUDController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Shrine") return; //update only if in shrine scene

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            abilityState = stasisAbility;
            abilityState.setAbilityActive();
            ability_text.SetText("Ability Activated: Stasis");
            HUDController.updateAbilityUI(4);


        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            abilityState = cryonisAbility;
            abilityState.setAbilityActive();
            ability_text.SetText("Ability Activated: Cryonis");
            HUDController.updateAbilityUI(2);

        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            abilityState = bombAbility;
            abilityState.setAbilityActive();
            ability_text.SetText("Ability Activated: Remote Bomb");
            HUDController.updateAbilityUI(1);

        }

        if (Keyboard.current.qKey.wasPressedThisFrame && abilityState != null)
        {
            bool abilitySucceeded = abilityState.performAbility();
            if (abilityState == bombAbility && abilitySucceeded)//ability state is not bomb ability until another bomb is thrown
            { abilityState = null; ability_text.SetText("Ability Activated: None"); HUDController.updateAbilityUI(10);
            }
        }

        //to fix bug of bombthrown being active although ability is performed
        if (abilityState != bombAbility && bombThrown.activeSelf) bombThrown.SetActive(false);

   

    }

    

}





