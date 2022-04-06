using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon to Spawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator playerAnimator;
    Sprite crosshairImage;
    WeaponComponent equippedWeapon;
    public WeaponComponent GetEquippedWeapon => equippedWeapon;

    [SerializeField]
    GameObject weaponSocketLocation;
    //[SerializeField]
    //Transform GripIKSocketLocation;

    bool firingPressed = false;
    GameObject spawnedWeapon;
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    [SerializeField]
    private WeaponScriptable startWeapon;

    [SerializeField]
    private WeaponAmmoUI weaponAmmoUI;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();

        ////spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        
        playerController.inventory.AddItem(startWeapon, 1);

        //startWeapon.UseItem(playerController);
        //EquipWeapon(startWeapon);
        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //equippedWeapon.Initialize(this, startWeapon);

        //PlayerEvents.InvokeOnWeaponEquipped(startWeapon.itemPrefab.GetComponent<WeaponComponent>());


        //GripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }

    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        StartReloading();
    }

    //private void OnAnimatorIK(int layerIndex)
    //{
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKSocketLocation.transform.position);
    //}

    private void StartFiring()
    {
        if (!equippedWeapon) return;
         
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        };

        playerController.isFiring = true;
        playerAnimator.SetBool(isFiringHash, true);
        equippedWeapon.StartFiringWeapon();
    }

    private void StopFiring()
    {
        if (!equippedWeapon) return;

        playerController.isFiring = false;
        playerAnimator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiringWeapon();
    }

    public void StartReloading()
    {
        if (!equippedWeapon) return;

        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;

        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0) return;
        playerAnimator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);

    }

    public void StopReloading()
    {
        if (!equippedWeapon) return;

        if (playerAnimator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        playerAnimator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);

        if (!spawnedWeapon) return;

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();

        if (!equippedWeapon) return;
        Debug.Log("Weapon Stats: " + equippedWeapon.weaponStats.weaponName);
       
        equippedWeapon.Initialize(this, weaponScriptable);
        //PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        weaponAmmoUI.OnWeaponEquipped(equippedWeapon);
    }

    public void UnquipWeapon()
    {
        if (!equippedWeapon) return;
        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
