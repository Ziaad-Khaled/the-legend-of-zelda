using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthController : MonoBehaviour
{
    private int healthPoints;
    private bool isInvincible;
    public static Animator animator; 
    public GameObject hud;
    private HUDController hUDController;
    [SerializeField] private AudioClip deathSoundClip;
    private static AudioClip deathSound;
    [SerializeField] private AudioClip hitSound;
    private CombatController combatController;
    public static bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 24;
        isInvincible = false;
        isGameOver = false;
        animator = this.GetComponent<Animator>();
        hUDController = hud.GetComponent<HUDController>();
        deathSound = deathSoundClip;
        combatController = this.GetComponent<CombatController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
            Heal();
        if (Keyboard.current.iKey.wasPressedThisFrame)
            ToggleInvincibility();
        if (Keyboard.current.tKey.wasPressedThisFrame)
            ToggleTimeScale();
    }

    void ToggleTimeScale()
    {
        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1.0f;
    }

    public void PerformDamage(int damagePoints)
    {
        if(CanPerformDamage())
        {
            healthPoints -= damagePoints;
            animator.Play("HitReaction");
            if(healthPoints>0)
                AudioManager.instance.PlaySFX(hitSound);

        }
            
        hUDController.updateHealthUI(healthPoints);
        if(healthPoints <= 0)
            GameOver();
    }

    public bool CanPerformDamage()
    {
        bool useShield = combatController.GetUseShield();
        bool healthGreaterThanZero = healthPoints > 0;
        return !isInvincible && !useShield && healthGreaterThanZero;
    }

    private void Heal()
    {
        healthPoints += 10;
        hUDController.updateHealthUI(healthPoints);
        Debug.Log("health points is " + healthPoints);
    }

    private void ToggleInvincibility()
    {
        isInvincible = !isInvincible;
    }

    public static void GameOver()
    {
        if (!isGameOver)
        {
            HealthController.animator.Play("Die");
            AudioManager.instance.PlaySFX(deathSound);
            isGameOver = true;
            
        }
    }
    public void GoToGameOver()
    {
        SceneChanger.instance.changeSceneGameover();
    }

    public void ResetHealth()
    {
        healthPoints = 24;
    }


}
