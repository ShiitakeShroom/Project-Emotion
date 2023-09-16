using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EmotionSystem;

[CreateAssetMenu(menuName = "Abilities/Projectil", fileName = "Projectil Ability")]
public class ProjectilSpawnAbility : BaseAbility
{
    public enum AbilityEnforcment
    {
        NormalProjectil,
        FireProjectil,
        IceProjectil,
        NoProjectil
    }

    public AbilityEnforcment Enforcment()
    {
        EmotionSystem emotion = FindObjectOfType<EmotionSystem>();

        if (HasAnyEmotionWithValue(emotion))
        {
            if (!affektedByAbility)
            {
                return AbilityEnforcment.NormalProjectil;
            }
        }
        if (HasAnyEmotionWithValue(emotion) && affektedByAbility)
        {
            if (iceAbility && !fireAbility)
            {
                return AbilityEnforcment.IceProjectil;
            }
            else if (!iceAbility && fireAbility)
            {
                return AbilityEnforcment.FireProjectil;
            }
        }
        return AbilityEnforcment.NoProjectil;
    }
    [Header("projectilSystem")]
    public GameObject normalProjektil;
    public GameObject iceProjectil;
    public GameObject fireProjectil;
    public Transform projectilSpawn;

    [Header("Boolans")]
    public bool affektedByAbility = false;
    public bool iceAbility = false;
    public bool fireAbility = false;

    [Header("DebuffDatas")]
    [SerializeField] private float projectilCost = 3f;
    [SerializeField] private float attackSpeedDebuff = 0.85f;


    public override void Activate(AbilityHolder holder)
    {
        projectilSpawn = holder.transform.Find("ProjektilSpawn");
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        switch (Enforcment())
        {
            case AbilityEnforcment.NormalProjectil:
                Instantiate(normalProjektil, projectilSpawn.position, projectilSpawn.rotation);
                emotionSystem.ConsumeEmotionAsResources(1, projectilCost);
                break;
            case AbilityEnforcment.IceProjectil:
                Instantiate(iceProjectil, projectilSpawn.position, projectilSpawn.rotation);
                emotionSystem.ConsumeEmotionAsResources(2, projectilCost);
                AddIceDebuff();
                break;
            case AbilityEnforcment.FireProjectil:
                Instantiate(fireProjectil, projectilSpawn.position, projectilSpawn.rotation);
                emotionSystem.ConsumeEmotionAsResources(2, projectilCost);
                AddFireDebuff();
                break;
            case AbilityEnforcment.NoProjectil:
                Debug.Log("not enough emotions");
                break;
        }
        
    }

    public bool HasAnyEmotionWithValue(EmotionSystem emotionSystem)
    {
        // Erstelle eine Kopie der Emotionen, um die Sortierung vorzunehmen.
        FloatValue[] sortedEmotions = emotionSystem.emotionValues.ToArray();

        // Sortiere die Emotionen nach ihrem Wert in absteigender Reihenfolge.
        sortedEmotions = sortedEmotions.OrderByDescending(e => e.value).ToArray();

        // Überprüfe den höchsten Wert der Emotionen und vergleiche ihn mit skillCost.
        if (sortedEmotions.Length > 0 && sortedEmotions[0].value >= skillCost)
        {
            return true;
        }

        return false;
    }

    void AddIceDebuff()
    {
        BuffManager buffManager = FindObjectOfType<BuffManager>();

        BuffData IceDebuff = new BuffData(
        "EmotionBuff", //name
        12f, //duration
        1f, //attack
        attackSpeedDebuff, //attackSpeed
        2.5f, //defenceModifier
        1f, //speed
        1f, //healthregen
        1f, //mindregen
        0f
        ); //skillattack

        buffManager.Addbuff(IceDebuff);
    }

    void AddFireDebuff()
    {
        BuffManager buffManager = FindObjectOfType<BuffManager>();
        PlayerHealth player = FindObjectOfType<PlayerHealth>();

        player.DecreaseHealth(3f);
        BuffData FireDebuff = new BuffData(
        "EmotionBuff", //name
        12f, //duration
        2f, //attack
        1.2f, //attackSpeed
        0.85f, //defenceModifier
        1.2f, //speed
        1f, //healthregen
        1f, //mindregen
        0f
        ); //skillattack

        buffManager.Addbuff(FireDebuff);
    }
}
