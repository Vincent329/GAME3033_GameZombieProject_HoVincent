using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI currentClipAmmoText;
    [SerializeField] private TextMeshProUGUI totalAmmoText;
    
    [SerializeField] WeaponComponent weaponComponent;

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += this.OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= this.OnWeaponEquipped;

    }

    public void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentClipAmmoText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalAmmoText.text = weaponComponent.weaponStats.totalBullets.ToString();


    }
}
