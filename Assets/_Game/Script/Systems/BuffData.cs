using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffData
{
    public string name; //name des Buffes
    public float duration;// wie lange der Buff ist 
    public float attackModifier;//um wieviel er sich erhöht
    public float attackSpeedModifier;
    public float defenceModifier;//um wieviel er sich erhöht
    public float speedModifier;//um wieviel er sich erhöht
    public float healthregenModifier;//um wieviel er sich erhöht
    public float mindregenModifier;//um wieviel er sich erhöht
    public float skillAttackDamageModifier;//um wieviel er sich erhöht


    public BuffData(string name, float duration, float attackModifier, float attackSpeedModifier, float defenceModifier, float speedModifier, float healthregenModifier, float mindregenModifier, float skillAttackDamageModifier)
    {
        this.name = name;
        this.duration = duration;
        this.attackModifier = attackModifier;
        this.attackSpeedModifier = attackSpeedModifier;
        this.defenceModifier = defenceModifier;
        this.speedModifier = speedModifier;
        this.healthregenModifier = healthregenModifier;
        this.mindregenModifier = mindregenModifier;
        this.skillAttackDamageModifier = skillAttackDamageModifier;
    }
}
