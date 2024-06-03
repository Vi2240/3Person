using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/RocketWeapon")]

public class RocketScriptableObject : ScriptableObject
{
    public float launchSpeed = 10f;
    public float turnSpeed = 5f;
    public float selfPropelledSpeed = 25f;
    public float propulsionDelay = 2f;
    public float explosionTime;
    public float noTargetTime;
    public float maxRange = 50f; // Maximum range for the guided rocket

    public float coneRadius = 10f; // Radius of the cone
    public float coneAngle = 45f; // Half-angle of the cone
}
