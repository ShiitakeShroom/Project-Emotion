using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class AbilityHolder : MonoBehaviour
{
    //Who owns the Ability
    public Character owner;

    //the Ability to Trigger
    public BaseAbility Ability;

    //Currentstate used to determain what to do once the ability is triggerd.
    public AbilityStates CurrentAbilityState = AbilityStates.ReadyToUse;

    private IEnumerator _handleAbilityUsage;

    //Unity Event to Add custom behaviors from editor code once ability is triggerd.
    public UnityEvent OnTriggerAbility;

    public enum AbilityStates
    {
        ReadyToUse = 0,
        Casting = 1,
        CoolDown = 2
    }

    public bool HasRequiredEmotions(EmotionSystem emotionSystem)
    {
        foreach (EmotionSystem.EmotionType emotionType in Ability.requiredEmotions)
        {
            //Überprüfe ob die benötigten Emotionen vorhanden sind
            float emotionValue = emotionSystem.GetEmotionValue(emotionType);

            //Ändere die Bedinung nach anfroderung
            if (emotionValue < Ability.skillCost)
            {
                return false;
            }
        }
        return true;
    }

    //Triggers the ability.
    public void TriggerAbility()
    {
        EmotionSystem emotionSystem = FindObjectOfType<EmotionSystem>();
        //the ability can only be trigger if its current state is readytouse.
        if (CurrentAbilityState != AbilityStates.ReadyToUse)
            return;

        //if the Character is not in allowed state then we avoid triggering the ability.
        if (!CharacterIsOnAllowedState())
            return;

        if (!HasRequiredEmotions(emotionSystem))
        {
            return;
        }

        //we start the process of triggering the ability
        StartCoroutine(HandleAbilityUsage_CO());
    }

    public bool CharacterIsOnAllowedState()
    {
        return Ability.AllowedCharacterStates.Contains(owner.CurrentCharacterStates);
    }

    private IEnumerator HandleAbilityUsage_CO()
    {
        //Sets the ability in casting State
        CurrentAbilityState = AbilityStates.Casting;
        Debug.Log("Casting it");

        //wait fpor casting time
        yield return new WaitForSeconds(Ability.CastingTime);

        //triggers the actual ability behavior.
        Ability.Activate(this);

        //sets the ability on cooldown state
        CurrentAbilityState = AbilityStates.CoolDown;
        Debug.Log("casttime" + AbilityStates .CoolDown);

        //invoking unity methoid
        OnTriggerAbility?.Invoke();

        //if has cooldDown, handle it.
        if(Ability.HasCooldown)
        {
            StartCoroutine(HandleCooldown_CO());
        }
    }

    private IEnumerator HandleCooldown_CO()
    {
        //Wait for coolddowntime
        yield return new WaitForSeconds(Ability.CooldDown);

        //Sets ability ready to use.
        CurrentAbilityState = AbilityStates.ReadyToUse;
    }
}
