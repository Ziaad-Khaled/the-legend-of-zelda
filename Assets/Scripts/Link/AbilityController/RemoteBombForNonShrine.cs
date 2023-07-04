using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RemoteBombForNonShrine : MonoBehaviour
{
    AbilityState bombAbility;
    public GameObject bombInHand;
    public GameObject bombThrown;
    public Animator ThirdPersonAnimator;
    public ParticleSystem explosionEffect;
    public AudioClip explosionSound;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        bombAbility = new RemoteBombAbility(bombThrown, bombInHand, explosionEffect, ThirdPersonAnimator, explosionSound);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Shrine") return; //do not update if in shrine scene

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            bombAbility.setAbilityActive();
            isActive = true;
        }

        if (Keyboard.current.qKey.wasPressedThisFrame && isActive)
        {
            bool abilitySucceeded =  bombAbility.performAbility();
            if(abilitySucceeded) isActive = false;
        }
        if (!isActive && bombThrown.activeSelf) bombThrown.SetActive(false);

    }
}
