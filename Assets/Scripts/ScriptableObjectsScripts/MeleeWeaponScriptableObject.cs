using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/MeleeWeapon")]

public class MeleeWeaponScriptableObject : ScriptableObject
{
    public int x, y, z;

    public int damage;
}
