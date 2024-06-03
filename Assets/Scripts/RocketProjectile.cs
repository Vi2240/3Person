using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    [SerializeField] RocketScriptableObject rocketscriptableObject;

    [SerializeField] GameObject thrustEffect;

    [SerializeField] private LayerMask enemyLayer;

    private bool selfPropelled = false;
    private bool collide = false;
    private float startTime;

    private Transform target;
    private Rigidbody rb;

    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustEffect.SetActive(false);
        startTime = Time.time;
        LaunchRocket();
    }

    public override void Update()
    {
        if (selfPropelled && collide != true)
        {
            FindTargetInCone();
            GuidedMoveRocket(rocketscriptableObject.selfPropelledSpeed);
        }
    }

    void LaunchRocket()
    {
        rb.velocity = transform.forward * rocketscriptableObject.launchSpeed;
        Invoke("StartSelfPropulsion", rocketscriptableObject.propulsionDelay);
    }

    void StartSelfPropulsion()
    {
        selfPropelled = true;
    }

    void FindTargetInCone()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rocketscriptableObject.maxRange, enemyLayer);

        foreach (var hitCollider in hitColliders)
        {
            Vector3 directionToTarget = hitCollider.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);

            // Check if the target is within the cone based on both radius and angle
            if (angle <= rocketscriptableObject.coneAngle && directionToTarget.magnitude <= rocketscriptableObject.coneRadius)
            {
                target = hitCollider.transform;
                return; // Exit the loop after finding the first valid target
            }
        }

        // If no valid target is found, set target to null
        target = null;
    }

    void MoveForward(float speed)
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    void GuidedMoveRocket(float speed)
    {
        thrustEffect.SetActive(true);

        if (target != null)
        {
            StopCoroutine(NoTarget());

            // Get the direction to the target
            Vector3 targetDirection = target.position - transform.position;

            // Calculate the rotation step
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, rocketscriptableObject.turnSpeed * Time.deltaTime, 0.0f);

            // Rotate towards the target
            transform.rotation = Quaternion.LookRotation(newDir);

            // Move forward
            MoveForward(speed);

            // Check if the rocket has reached or exceeded the maximum range
            if (Vector3.Distance(transform.position, target.position) >= rocketscriptableObject.maxRange)
            {
                // Deactivate the rocket or initiate destruction
                StartCoroutine(Collide());
            }
        }
        else
        {
            if(selfPropelled)
            {
                StartCoroutine(NoTarget());
            }
        }
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            return;
        }
        base.OnCollisionEnter(other);

        StartCoroutine(Collide());
    }

    public IEnumerator Collide()
    {
        thrustEffect.SetActive(false);
        collide = true;
        GuidedMoveRocket(0);

        yield return new WaitForSeconds(rocketscriptableObject.explosionTime);

        Destroy(gameObject);
    }

    public IEnumerator NoTarget()
    {
        GuidedMoveRocket(rocketscriptableObject.selfPropelledSpeed);

        yield return new WaitForSeconds(rocketscriptableObject.noTargetTime);

        StartCoroutine(Collide());
    }

    private void OnDrawGizmos()
    {
        if (selfPropelled && !collide)
        {
            DrawConeGizmo(transform.position, transform.forward);
        }
    }

    private void DrawConeGizmo(Vector3 missilePosition, Vector3 missileDirection)
    {
        Gizmos.color = Color.yellow;

        // Calculate the rotation from the forward direction to the specified direction
        Quaternion rotation = Quaternion.LookRotation(missileDirection, Vector3.up);

        // Calculate the position in front of the missile
        Vector3 conePosition = missilePosition + missileDirection * rocketscriptableObject.coneRadius;

        // Draw the wireframe cone using Gizmos.DrawWireMesh
        Gizmos.matrix = Matrix4x4.TRS(conePosition, rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, rocketscriptableObject.coneAngle * 2f, rocketscriptableObject.coneRadius, 0f, 1f);

        // Reset the Gizmos.matrix
        Gizmos.matrix = Matrix4x4.identity;
    }
}
