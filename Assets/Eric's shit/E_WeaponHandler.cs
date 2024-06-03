using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;
/*
public enum WeaponState
{
    Unarmed,
    HitScan,
    Projectile,
    Chargeable,
    Total
}*/

public class E_WeaponHandler : MonoBehaviour
{
    /*public PlayerMovement myPlayerMovement;
    
    [Header("Unarmed = Element 0 \n" + "Hitscan = Element 1 \n" + "Projectile = Element 2 \n" + "Chargeable = Element 3")]
    
    [SerializeField] Weapon[] availableWeapons = new Weapon[(int)WeaponState.Total];
    [SerializeField] Weapon currentWeapon = null;
    [SerializeField] ChargeableWeapon chargeableWeapon;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] float mouseAxisBreakpoint = 1;

    float mouseAxisDelta;

    public virtual void Update()
    {
        HandleWeaponSwap();

        if (Input.GetMouseButtonDown(0) && currentWeapon != null && currentWeapon.weaponType != WeaponState.Chargeable && mainMenuCanvas.activeSelf == false)
        {
            //chargeableWeapon.SetChargeableToUnequipped();
            currentWeapon.Fire();
            DisplayAmmo();
        }
    }

    void HandleWeaponSwap()
    {
        mouseAxisDelta += Input.mouseScrollDelta.y;

        if (Mathf.Abs(mouseAxisDelta) > mouseAxisBreakpoint)
        {
            int swapDirection = (int)Mathf.Sign(mouseAxisDelta);
            mouseAxisDelta -= swapDirection * mouseAxisBreakpoint;

            int currentWeaponIndex = (int)currentWeapon.weaponType;
            currentWeaponIndex += swapDirection;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + currentWeaponIndex;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            currentWeapon.gameObject.SetActive(false);
            currentWeapon = availableWeapons[currentWeaponIndex];
            currentWeapon.gameObject.SetActive(true);
            DisplayAmmo();
        }
    }

    public void AddAmmo(int ammoToAdd, bool wantMaxAmmo)
    {
        foreach (Weapon allWeapons in availableWeapons)
        {
            if (wantMaxAmmo)
            {
                //Set every weapons ammo to max
                allWeapons.Ammunition = allWeapons.maxAmmunition;
            }
            else
            {
                allWeapons.Ammunition += ammoToAdd;

                if (allWeapons.Ammunition > allWeapons.maxAmmunition)
                {
                    allWeapons.Ammunition = allWeapons.maxAmmunition;
                }
            }
        }

        DisplayAmmo();
    }

    public void DisplayAmmo()
    {
        ammoCountText.text = currentWeapon.Ammunition.ToString();
    }*/
}
