using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectil", fileName = "Projectil Ability")]
public class ProjectilSpawnAbility : BaseAbility
{
    public GameObject projektil;
    public Transform projectilSpawn;
    public override void Activate(AbilityHolder holder)
    {
        projectilSpawn = holder.transform.Find("ProjektilSpawn");
        Instantiate(projektil, projectilSpawn.position, projectilSpawn.rotation);
    }
}
