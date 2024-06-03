using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]

public class EnemyScriptableObject : ScriptableObject
{
    public int maxHealth = 100;
    public float speed = 5;

    public EnemyAttackScriptableObject enemyAttackType;
}
