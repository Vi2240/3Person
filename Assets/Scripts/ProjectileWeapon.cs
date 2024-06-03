using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public Projectile projectileToSpawn = null;

    public override bool Fire()
    {
        if (!base.Fire())
        {
            return false;
        }

        // Calculate the aim point from the center of the camera's viewport
        //Vector3 aimPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + 10000f));
        Vector3 aimPoint = mainCam.transform.forward.normalized;

        Debug.Log("Aim Point: " + aimPoint);
        Debug.Log("Weapon Position: " + transform.position);

        // Instantiate the projectile at the weapon's position and initialize it
        Projectile spawnedProjectile = Instantiate(projectileToSpawn, transform.position, Quaternion.identity);
        spawnedProjectile.gameObject.SetActive(true);
        spawnedProjectile.Init(aimPoint);

        return true;
    }
}
