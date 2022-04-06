using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;
    public bool isInventoryOn = false;

    public InventoryComponent inventory;
    public WeaponHolder weaponHolder;
    public GameUIController gameUIController;

    private void Awake()
    {
        if (inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();
        }
        
        if (weaponHolder == null)
        {
            weaponHolder = GetComponent<WeaponHolder>();
        }

        if (gameUIController == null)
        {
            gameUIController = FindObjectOfType<GameUIController>();
        }

    }

    public void OnInventory(InputValue value)
    {
        isInventoryOn = !isInventoryOn;
        gameUIController.ToggleInventory(isInventoryOn);
        AppEvents.InvokeOnMouseCursorEnable(isInventoryOn);
    }
}
