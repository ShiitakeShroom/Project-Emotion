using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public CharacterStatus playerStatus;
    private List<BuffData> activeBuffs = new List<BuffData>();

    //Metheode zum Hinzufügen von Buffs zu einem Object
    public void Addbuff(BuffData buff)
    {
        activeBuffs.Add(buff);
        StartCoroutine(RemoveBuffAfterDuration(buff));
        ApplyBuffEffects();
    }

    //Methofe zum Enterfenen eines Buffes aus der Liste
    public void RemoveBuffFromList(BuffData buff)
    {
        activeBuffs.Remove(buff);
    }

    //methode zum Anwedner der Buff-Effekte auf das Objekt
    private void ApplyBuffEffects()
    {   
        //wenn was gebufft wird wird es mit 1 multipliziert nicht mit 0, da sonst komplikationen auftauchen 
        //attack
        float totalAttackModifier = 1f;
        //attackspeed
        float totalAttackSpeedModifier = 1f;
        //Defence
        float totalDefenceModifier = 1f;
        //Speed
        float totalSpeedModifier = 1f;
        //healthregen
        float totalHealthregenModifier = 1f;
        //mindregn
        float totalMindregenModifier = 1f;
        //Skilldamage
        float totalSkillattackDamageModifier = 1f;

        foreach(BuffData buff in activeBuffs)
        {
            //attack
            totalAttackModifier *= buff.attackModifier;
            //defence
            totalDefenceModifier *= buff.defenceModifier; 
            //Speed
            totalSpeedModifier *= buff.speedModifier;
            //attackSpeed
            totalAttackSpeedModifier *= buff.attackSpeedModifier;
            //HealthRegen
            totalHealthregenModifier *= buff.healthregenModifier;
            //Mindregen
            totalMindregenModifier *= buff.mindregenModifier;
            //skilldamage 
            //Skill damage wird nur bei Skills auf ganen Zahlen zu der Zahl zahl 0 hinzugerechnet
            totalSkillattackDamageModifier += buff.skillAttackDamageModifier;
        }

        //anwenden der gesammlten Buffmodifier auf die Statistik
        playerStatus.attack *= totalAttackModifier;
        playerStatus.defence *= totalDefenceModifier;
        playerStatus.speed *= totalSpeedModifier;
        playerStatus.attackSpeed *= totalAttackSpeedModifier;
        playerStatus.healthregen *= totalHealthregenModifier;
        playerStatus.mindregen *= totalMindregenModifier;
        playerStatus.skillAttackDamage += totalSkillattackDamageModifier;
    }

    private void RemoveBuffs()
    {
        //wenn was gebufft wird wird es mit 1 multipliziert nicht mit 0, da sonst komplikationen auftauchen 
        //attack
        float totalAttackModifier = 1f;
        //attackspeed
        float totalAttackSpeedModifier = 1f;
        //Defence
        float totalDefenceModifier = 1f;
        //Speed
        float totalSpeedModifier = 1f;
        //healthregen
        float totalHealthregenModifier = 1f;
        //mindregn
        float totalMindregenModifier = 1f;
        //Skilldamage
        float totalSkillattackDamageModifier = 1f;

        foreach (BuffData buff in activeBuffs)
        {
            //attack
            totalAttackModifier *= buff.attackModifier;
            //defence
            totalDefenceModifier *= buff.defenceModifier;
            //Speed
            totalSpeedModifier *= buff.speedModifier;
            //attackSpeed
            totalAttackSpeedModifier *= buff.attackSpeedModifier;
            //HealthRegen
            totalHealthregenModifier *= buff.healthregenModifier;
            //Mindregen
            totalMindregenModifier *= buff.mindregenModifier;
            //skilldamage 
            //Skill damage wird nur bei Skills auf ganen Zahlen zu der Zahl zahl 0 hinzugerechnet
            totalSkillattackDamageModifier += buff.skillAttackDamageModifier;
        }

        //anwenden der gesammlten Buffmodifier auf die Statistik
        playerStatus.attack /= totalAttackModifier;
        playerStatus.defence /= totalDefenceModifier;
        playerStatus.speed /= totalSpeedModifier;
        playerStatus.attackSpeed /= totalAttackSpeedModifier;
        playerStatus.healthregen /= totalHealthregenModifier;
        playerStatus.mindregen /= totalMindregenModifier;
        playerStatus.skillAttackDamage /= totalSkillattackDamageModifier;
    }


    private IEnumerator RemoveBuffAfterDuration(BuffData buff)
    {
        yield return new WaitForSeconds(buff.duration);
        RemoveBuffs();
        RemoveBuffFromList(buff);
    }
}
