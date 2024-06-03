using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    [SerializeField] HitScanWeaponScriptableObject hitScanWeaponScriptableObject;

    [Header("VisualEffekts")]
    [SerializeField] private ParticleSystem hitParticle = null;
    [SerializeField] private GameObject muzzleFlash = null;
    [SerializeField] private float muzzleFlashDuration = 0.5f; // Duration of the muzzle flash in seconds
    [SerializeField] private float muzzleFlashTimer = 0f; // Timer to track the duration of the muzzle flash

    [Header("Sound")]
    [SerializeField] private AudioSource fireSound = null;
    [SerializeField] private AudioClip fireSoundClip = null;
    [SerializeField] private float fireSoundDuration = 0.5f; // Duration of the fire sound in seconds
    [SerializeField] private float fireSoundTimer = 0f; // Timer to track the duration of the fire sound

    [SerializeField] private GameObject realMag;
    [SerializeField] private GameObject fakeMag;

    private Camera mainCamera;

    protected new void Start()
    {
        base.Start();
        mainCamera = Camera.main;

        if (hitParticle == null)
        {
            Debug.LogError("HitParticle is not assigned in HitScanWeapon.");
        }
        if (muzzleFlash == null)
        {
            Debug.LogError("MuzzleFlash is not assigned in HitScanWeapon.");
        }

        muzzleFlash.SetActive(false);
    }

    protected new void Update()
    {
        base.Update();

        if (muzzleFlash.activeSelf)
        {
            muzzleFlashTimer += Time.deltaTime; // Increment the timer

            if (muzzleFlashTimer >= muzzleFlashDuration)
            {
                muzzleFlash.SetActive(false); // Deactivate the muzzle flash after the duration
            }
        }

        if (fireSound.isPlaying)
        {
            fireSoundTimer += Time.deltaTime; // Increment the timer

            if (fireSoundTimer >= fireSoundDuration)
            {
                fireSound.Stop(); // Stop the fire sound after the duration
            }
        }
    }

    public override bool Fire()
    {
        if (base.Fire() == false)
        {
            return false;
        }
        HitScanFire();
        return true;
    }

    public override void RunReloadAnimation()
    {
        StartCoroutine(ReloadAnimation());
    }

    private void HitScanFire()
    {
        MuzzleFlash();
        PlayFireSound();

        Ray weaponRay = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(weaponRay, out hit, GetWeaponRange, ~GetIgnoreHitMask))
        {
            if (hitParticle != null)
            {
                PlayHitParticle(hit);
            }

            var tryEnemy = hit.transform.gameObject.GetComponent<Enemy>();
            if(tryEnemy != null)
            {
                tryEnemy.TakeDamage(hitScanWeaponScriptableObject.damage);
            }
            HandleEntityHit(hit);
        }
    }

    private void PlayHitParticle(RaycastHit hit)
    {
        hitParticle.transform.position = hit.point;
        hitParticle.transform.forward = hit.normal;
        hitParticle.transform.Translate(hit.normal.normalized * 0.1f);
        hitParticle.Play();
    }

    private void HandleEntityHit(RaycastHit hit)
    {
        var playerHit = hit.transform.gameObject.GetComponent<Player>();
        /*if (playerHit != null)
        {
            Debug.Log("Player hit!");
        }
        else
        {
            Debug.Log("Other entity hit!");
        }*/
    }

    private void MuzzleFlash()
    {
        if (!muzzleFlash.activeSelf)
        {
            muzzleFlash.SetActive(true);
            muzzleFlashTimer = 0f; // Reset the timer
        }
    }

    private void PlayFireSound()
    {
        if (fireSound != null && fireSoundClip != null)
        {
            fireSound.PlayOneShot(fireSoundClip);
            fireSoundTimer = 0f; // Reset the timer
        }
    }

    IEnumerator ReloadAnimation()
    {
        Instantiate(fakeMag, realMag.transform.position, Quaternion.identity);
        realMag.SetActive(false);

        yield return new WaitForSeconds(GetReloadeTime);

        realMag.SetActive(true);
    }
}