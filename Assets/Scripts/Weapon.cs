using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isMelee = false;
    [SerializeField] int maxMagAmmo = 30; 
    [SerializeField] int maxAmmo = 300;
    [SerializeField] float weaponRange = 13337.0f;
    [SerializeField] float reloadeTime = 1.5f;

    [SerializeField] WeaponHandler holdingWeaponHandler = null;
    [SerializeField] WeaponState weaponType = WeaponState.Total;
    [SerializeField] LayerMask ignoreHitMask = 0;

    int ammo;
    int magAmmo;

    bool isReloading;

    //Properties for read access to private fields
    public WeaponState WeaponType => weaponType;
    public int GetAmmo => ammo;
    public int GetMagAmmunition => magAmmo;
    public float GetReloadeTime => reloadeTime;
    public float GetWeaponRange => weaponRange;
    public LayerMask GetIgnoreHitMask => ignoreHitMask;

    protected Camera mainCam;

    WeaponHandler weaponHandler;

    protected void Start()
    {
        mainCam = Camera.main;

        if (!isMelee)
        {
            magAmmo = maxMagAmmo;
            ammo = maxAmmo;
        }

        weaponHandler = FindObjectOfType<WeaponHandler>();
    }

    protected void Update()
    {
        if(!isMelee)
        {
            if (Input.GetKeyDown(KeyCode.R) && magAmmo < maxAmmo && ammo > 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }
     
    }

    // Method to simulate firing the weapon
    public virtual bool Fire()
    {
        if (!isMelee)
        {
            // Check if there is ammunition
            if (magAmmo < 1 || isReloading)
            {
                return false;
            }

            // Decrement ammunition and return true to indicate successful firing
            magAmmo--;
            return true;
        }
        return false;
    }

    public virtual void RunReloadAnimation()
    {

    }

    IEnumerator Reload()
    {
        isReloading = true;
        RunReloadAnimation();
        yield return new WaitForSeconds(reloadeTime);

        int requiredAmmo = maxMagAmmo - magAmmo;
        int ammoToReload = Mathf.Min(requiredAmmo, ammo); // Relaod available ammo or until magazine is full

        magAmmo += ammoToReload;
        ammo -= ammoToReload;

        weaponHandler.DisplayAmmo();

        isReloading = false;
    }
}
