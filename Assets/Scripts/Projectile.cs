using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectileObject = null;
    public GameObject detonationObject = null;

    public float startMovementSpeed;

    public Vector3 spawnPos = Vector3.zero;
    public Vector3 aimPoint = Vector3.zero;

    protected Vector3 aimDirection = Vector3.zero;

    [Header("LifeTime")]
    public float detonationLifeTime = -1337.0f;
    public float detonationMaxLifeTime = 0;
    protected float lifeTime = 0;
    protected float lifeTimeMax = 20f;

    public bool collided;

    public virtual void Start()
    {
        projectileObject.SetActive(true);
        if (detonationObject != null)
        {
            detonationObject.SetActive(false);
        }
    }

    public virtual void Update()
    {
        TryToHandleDetonationLifeTime();
        LifeTime();
    }

    public void LifeTime()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= lifeTimeMax)
        {
            Destroy(gameObject);
        }
    }

    public void TryToHandleDetonationLifeTime()
    {
        if (detonationObject != null && detonationLifeTime > 0.0f)
        {
            detonationLifeTime -= Time.deltaTime;
            if (detonationLifeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void Init(Vector3 aAimPoint)
    {
        aimPoint = aAimPoint;
        spawnPos = transform.position;

        aimDirection = (aimPoint - spawnPos).normalized;
        transform.LookAt(aimDirection);
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            return;
        }

        collided = true;

        if (detonationObject != null)
        {
            detonationLifeTime = detonationMaxLifeTime;
            detonationObject.SetActive(true);
        }

        //projectileObject.SetActive(false);
    }
}
