using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyAttack")]

public class EnemyAttackScriptableObject : ScriptableObject
{
    public int damage = 10;
    public float attackCooldown = 2;

    public float aggroRange = 10f;
    public float attackRange = 2f;
}
