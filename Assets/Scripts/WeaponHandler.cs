using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Xml.Linq;

public enum WeaponState
{
    Melee,
    HitScan,
    HitScan2,
    //Projectile,
    Total
}

public class WeaponHandler : MonoBehaviour
{
    [Header("Melee = Element 0 \n" + "Hitscan = Element 1 \n" + "Hitscan = Element 2 \n" + "Projectile = Element 3")]

    [SerializeField] float mouseAxisBreakpoint = 1;
    [SerializeField] float ScrollWheelBreakpoint = 1.0f;

    [SerializeField] Weapon[] AvailableWeapons = new Weapon[(int)WeaponState.Total];

    [SerializeField] Weapon CurrentWeapon = null;
    [SerializeField] TextMeshProUGUI ammoCounterText;

    float ScrollWheelDelta = 0.0f;

    public void Start()
    {
        int currentWeaponIndex = (int)CurrentWeapon.WeaponType;
    }

    public void Update()
    {
        HandleWeaponSwap();

        if (Input.GetMouseButtonUp(0) && CurrentWeapon != null)
        {
            CurrentWeapon.Fire();
            DisplayAmmo();
        }
    }

    private void HandleWeaponSwap()
    {

        ScrollWheelDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(ScrollWheelDelta) > ScrollWheelBreakpoint)
        {

            int swapDirection = (int)Mathf.Sign(ScrollWheelDelta);
            ScrollWheelDelta = 0.0f;

            int currentWeaponIndex = (int)CurrentWeapon.WeaponType;
            currentWeaponIndex += swapDirection;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + -1;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            CurrentWeapon.gameObject.SetActive(false);
            CurrentWeapon = AvailableWeapons[currentWeaponIndex];
            CurrentWeapon.gameObject.SetActive(true);
            DisplayAmmo();
        }
    }

    public void DisplayAmmo()
    {
        ammoCounterText.text = CurrentWeapon.GetMagAmmunition.ToString() + "/" + CurrentWeapon.GetAmmo.ToString();
    }
}
