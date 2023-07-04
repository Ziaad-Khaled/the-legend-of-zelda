using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;

public class CombatController : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    CombatState combatState;
    MeleeState meleeState;
    RangedState rangedState;
    public List<GameObject> meleeObjects;
    public List<GameObject> rangedObjects;
    public CinemachineVirtualCamera vCam;
    public GameObject arrow;
    public GameObject rightHand;
    public GameObject spine;
    public GameObject leftHand;
    public float zoomedInFov = 20;
    public float zoomedOutFov;
    public float currentFov = 20;
    public float fovSmoothSpeed = 10;
    [HideInInspector]
    public ThirdPersonController thirdPersonController;
    public GameObject hud;
    private HUDController hUDController;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    private bool useShield = false;

    public bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        zoomedOutFov = vCam.m_Lens.FieldOfView;
        currentFov = zoomedOutFov;
        animator = this.GetComponent<Animator>();
        meleeState = new MeleeState(this);
        rangedState = new RangedState(this);
        combatState = meleeState;
        thirdPersonController = this.GetComponent<ThirdPersonController>();
        hUDController = hud.GetComponent<HUDController>();
        hUDController.updateWeaponUI("melee");
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ChangeCombatState();
        }

        if(Input.GetMouseButtonDown(0))
        {
            combatState.PerformLeftMouseAction();
        }
     
        if (Input.GetMouseButtonDown(1))
        {
            combatState.PerformRightMouseDown();           
        }

        if (Input.GetMouseButtonUp(1))
        {
            combatState.PerformRightMouseUp();          
        }

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        //tranform player (only if is aiming ) (can also do it when not moving)
        if (rangedState.isAiming == true)
        {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            if (v > 0.1) v = 0.1f;
            if (v < -0.1) v = -0.1f;
            transform.Rotate(-v, h, 0);
        }
    }

    public void EnableShooting()
    {
        this.canShoot = true;
    }
    public void DisableShooting()
    {
        this.canShoot = false;
    }
    void ChangeCombatState()
    {

        if(combatState == meleeState)
        {
            ActivateGameObjects(rangedObjects);
            DeactivateGameObjects(meleeObjects);
            combatState = rangedState;
            hUDController.updateWeaponUI("ranged");
        }
        else
        {
            ActivateGameObjects(meleeObjects);
            DeactivateGameObjects(rangedObjects);
            combatState = meleeState;
            hUDController.updateWeaponUI("melee");
        }
    }

    void ActivateGameObjects(List<GameObject> list)
    {
        foreach (GameObject gameObject in list) 
        {
            gameObject.SetActive(true);
        }
    }

    void DeactivateGameObjects(List<GameObject> list)
    {
        foreach (GameObject gameObject in list) 
        {
            gameObject.SetActive(false);
        }
    }

    public bool CanDoAnimation()
    {
        return thirdPersonController.CanDoAnimation();
    }

    public bool GetUseShield()
    {
        return useShield;
    }

    public void SetUseShield(bool shield)
    {
        useShield = shield;
    }
}
