using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Combo")]
    public List<MeleeWeaponScriptableObject> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;

    bool isAttacking;

    [SerializeField] private LayerMask attackLayers = 0;

    protected new void Update()
    {
        if(Input.GetButtonDown("Fire1") && isAttacking == false)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
            if (Time.time - lastClickedTime >= 0.2f)
            {
                StartCoroutine(MeleeAttackAnimation());

                Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, transform.rotation, attackLayers);

                foreach (Collider collider in hitColliders)
                {
                    var enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(combo[comboCounter].damage);
                    }
                }

                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        if (Time.time - lastComboEnd > 5.0f)
        {

            comboCounter = 0;
            lastComboEnd = Time.time;
        }
    }

    IEnumerator MeleeAttackAnimation()
    {
        isAttacking = true;
        gameObject.transform.localRotation = Quaternion.Euler(combo[comboCounter].x, combo[comboCounter].y, combo[comboCounter].z);

        yield return new WaitForSeconds(0.2f);

        gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
        isAttacking = false;
    }
}
