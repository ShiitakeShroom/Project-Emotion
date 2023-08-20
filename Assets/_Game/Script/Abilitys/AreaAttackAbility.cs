using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(menuName = "Abilities/AreaAttack", fileName = "AreaAttack Ability")]
public class AreaAttackAbility : BaseAbility
{
    public float damageAmount = 100;
    public float currentDamageRadius = 50f;
    private EnemyHealth enemyHealth;
    private Animator animator;

    public void Awake()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
        

    }
    public override void Activate(AbilityHolder holder)
    {
        Collider[] colliders = Physics.OverlapSphere(holder.transform.position, currentDamageRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Feind"))
            {
                DealDamage(collider.gameObject);
                break; // Einmal Schaden verursachen und Schleife beenden.
            }
        }
    }

    private void DealDamage(GameObject target)
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DecreaseHealth(damageAmount);
        }
    }
}

