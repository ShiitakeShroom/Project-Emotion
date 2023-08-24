using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilitiesEnemy/Projectil", fileName ="ProjectilDmg Ability")]
public class EnemyProjectilAbility : BaseAbility
{
    public Transform projectilSpawn;
    public GameObject projectil;

    public override void Activate(AbilityHolder holder)
    {
        projectilSpawn = holder.transform.Find("ProjektilSpawn");
        Instantiate(projectil, projectilSpawn.position, projectilSpawn.rotation);
    }
}
