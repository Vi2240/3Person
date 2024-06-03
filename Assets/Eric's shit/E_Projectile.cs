using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Projectile : MonoBehaviour
{
    //public Entity holder;
    
    public GameObject projectileObject;
    public GameObject detonationObject;

    protected float detonationLifetime;
    public float detonationMaxLifetime;
    protected float lifetime;
    public float maxLifetime;
    
    protected Vector3 spawnPosition;
    protected Vector3 aimPoint;

    protected Vector3 aimDirection;

    public float movementSpeed;

    public int impactDamage;

    public void Start()
    {
        projectileObject.SetActive(true);
        if (detonationObject != null)
        {
            detonationObject.SetActive(false);
        }
        spawnPosition = transform.position;
        transform.position = spawnPosition;
    }

    public virtual void Update()
    {
        lifetime += Time.deltaTime;
        
        if (lifetime > maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Init(Vector3 aSpawnPosition, Vector3 anAimPoint)
    {
        spawnPosition = aSpawnPosition;
        aimPoint = anAimPoint;
        aimDirection = (aimPoint - spawnPosition).normalized;

        // Set rotation to look at aim point
        transform.LookAt(aimPoint);

        // Set position after rotation to avoid issues with rotation altering position
        transform.position = spawnPosition;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {   
        projectileObject.SetActive(false);
        
        if (detonationObject != null)
        {
            detonationLifetime = detonationMaxLifetime;
            detonationObject.SetActive(true);

            StartCoroutine(DespawnDetonationObject());
        }
    }

    // Coroutine to despawn the detonation object after a certain duration
    private IEnumerator DespawnDetonationObject()
    {
        yield return new WaitForSeconds(detonationLifetime);

        if (detonationObject != null)
        {
            detonationObject.SetActive(false);
        }
    }
}
