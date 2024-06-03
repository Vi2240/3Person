using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ProjectileWeapon : Weapon
{
    /*public Projectile projectileToSpawn;

    public override bool Fire()
    {
        if (!base.Fire())
        {
            return false;
        }

        SpawnProjectile();

        return true;
    }

    void SpawnProjectile()
    {
        // Calculate direction to fire based on camera's forward direction
        Vector3 direction = mainCam.transform.forward.normalized;

        // Spawn projectile with rotation facing the direction
        Projectile spawnedProjectile = Instantiate(projectileToSpawn, transform.position, Quaternion.LookRotation(direction));

        spawnedProjectile.Init(transform.position, transform.position + direction * 1000);
    }*/
}
