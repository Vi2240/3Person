using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyScriptableObject enemyScriptableObject;

    [SerializeField] private Transform wapon;

    public GameObject muzzleFlash = null;
    public float muzzleFlashDuration = 0.5f; // Duration of the muzzle flash in seconds
    public float muzzleFlashTimer = 0f; // Timer to track the duration of the muzzle flash

    private bool playerInRange = false;
    private bool canAttack = true; // Whether the enemy can attack
    private int currentHealth;

    private Transform player;
    private EnemyCounter enemyCounter;

    public bool isMelee;

    bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCounter = FindAnyObjectByType<EnemyCounter>();
        enemyCounter.modifeEnemysOnMap();
        currentHealth = enemyScriptableObject.maxHealth;
    }

    private void Update()
    {
        // Check if player is in aggro range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= enemyScriptableObject.enemyAttackType.aggroRange)
        {
            // Move towards the player if in range
            transform.LookAt(player);
            transform.Translate(Vector3.forward * enemyScriptableObject.speed * Time.deltaTime);

            // Check if player is within attack range and attack cooldown is over
            if (distanceToPlayer <= enemyScriptableObject.enemyAttackType.attackRange && canAttack)
            {
                AttackPlayer();
                StartCoroutine(AttackCooldown());
            }
        }

        if(!isMelee)
        {
            if (muzzleFlash.activeSelf)
            {
                muzzleFlashTimer += Time.deltaTime; // Increment the timer

                if (muzzleFlashTimer >= muzzleFlashDuration)
                {
                    muzzleFlash.SetActive(false); // Deactivate the muzzle flash after the duration
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void AttackPlayer()
    {
        // Deal damage to player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(enemyScriptableObject.enemyAttackType.damage);
        }

        if(isMelee)
        {
            StartCoroutine(RotateMeleeWeapon());
        }
        else
        {
            MuzzleFlash();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(!isDead)
        {
            enemyCounter.modifeEnemysKilled();

            isDead = true;
        }

        Destroy(gameObject);
    }

    private void MuzzleFlash()
    {
        if (!muzzleFlash.activeSelf)
        {
            muzzleFlash.SetActive(true);
            muzzleFlashTimer = 0f; // Reset the timer
        }
    }

    IEnumerator RotateMeleeWeapon()
    {
        // Rotate the axe to 90 degrees
        wapon.localRotation = Quaternion.Euler(0f, -90f, 90f);

        // Wait for 0.2 seconds
        yield return new WaitForSeconds(0.2f);

        // Rotate the axe back to 150 degrees
        wapon.localRotation = Quaternion.Euler(0f, -90f, 150f);
    }

    IEnumerator AttackCooldown()
    {
        // Set canAttack to false to prevent attacking
        canAttack = false;

        // Wait for attackCooldown seconds
        yield return new WaitForSeconds(enemyScriptableObject.enemyAttackType.attackCooldown);

        // Set canAttack to true to allow attacking again
        canAttack = true;
    }
}