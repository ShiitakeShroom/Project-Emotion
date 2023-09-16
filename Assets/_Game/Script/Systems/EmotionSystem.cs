using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using static EmotionSystem;
using static UnityEngine.Rendering.DebugUI;
using Unity.VisualScripting;
using static UnityEditor.ShaderData;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

public class EmotionSystem : MonoBehaviour
{
    public static EmotionSystem instance;

    [System.Serializable]
    public class FloatValue
    {
        public EmotionType emotionType;
        public float value;
    }
    //Define the Event, whenever a emotion will be changed
    public event EventHandler<EmotionChangedEventArgs> OnEmotionValueChanged;
    public event EventHandler maxEmotionValueChange;
    // Add a class to hold the event data for the emotion value change event
    public class EmotionChangedEventArgs : EventArgs
    {
        public EmotionType emotionType;
        public float newValue;
    }

    // Definiere die verschiedenen Emotionen
    public enum EmotionType { Wut, Traurig, Angst, Ekel, Glücklich, Überraschung }

    public event EventHandler NearlyMorbingTime;
    public event EventHandler OhNoItsMorbingTime;

    // Emotionswerte (von 0% bis 100%)
    public FloatValue[] emotionValues = new FloatValue[System.Enum.GetValues(typeof(EmotionType)).Length];

    // Maximaler Emotionswert
    public float maxEmotionValue = 100f;

    // Mindestwert einer Emotion
    public float minEmotionValue = 0f;

    // Schwelle, um sich in ein Monster zu verwandeln
    public float monsterTransformationThreshold = 75f;

    //rate wieviel emotioen eigentlich absorbiert werden
    public float absorptionMultiplier = 1.0f;

    private Coroutine consumeEmotionCoroutine;//die Coroutine muss gespeichert werden um damit sie gestop werden kann

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Methode zum Absorbieren einer Emotion von einem NPC oder Gegner

    public void AbsorbEmotion(EmotionObject.EmotionData[] emotions)
    {
        foreach (EmotionObject.EmotionData emotion in emotions)
        {
            int index = (int)emotion.emotionType;
            float adjustedValuetoAbsorb = emotion.valueToAbsorb * absorptionMultiplier;

            emotionValues[index].value += adjustedValuetoAbsorb;

            // Den Emotionswert innerhalb der Grenzen halten;
            emotionValues[index].value = Mathf.Clamp(emotionValues[index].value, minEmotionValue, maxEmotionValue);


            if (adjustedValuetoAbsorb > 0)
            {
                Debug.Log(emotion.emotionType.ToString() + " = " + emotionValues[index].value.ToString("F2") + "%");
            }

            OnEmotionValueChanged?.Invoke(this, new EmotionChangedEventArgs
            {
                emotionType = emotion.emotionType,
                newValue = emotionValues[index].value,
            });
        }
        CheckMonsterTransformation();
    }

    public void AddEmotionWithValue(EmotionType emotionType, float valueToAdd)
    {
        int index = (int)emotionType;
        float newValue = emotionValues[index].value + (valueToAdd * absorptionMultiplier);

        // Stelle sicher, dass der neue Wert zwischen minEmotionValue und maxEmotionValue liegt.
        newValue = Mathf.Clamp(newValue, minEmotionValue, maxEmotionValue);

        // Setze den neuen Emotionswert.
        SetEmotionValue(emotionType, newValue);
    }

    /*public void ConsumeEmotionAsResources(EmotionType[] requierdEmotiones, float resourceCost)
    {
        foreach (EmotionType emotionType in requierdEmotiones)
        {
            float newEmotionValue = GetEmotionValue(emotionType) - resourceCost;

            newEmotionValue = Mathf.Max(newEmotionValue, 0f);

            SetEmotionValue(emotionType, newEmotionValue);
        }
    }*/


    public void ConsumeEmotionAsResources(int numberOfEmotionsToUse, float resourceCost)
    {
        // Erstelle eine Kopie der Emotionen, um die Sortierung vorzunehmen.
        FloatValue[] sortedEmotions = emotionValues.ToArray();

        // Sortiere die Emotionen nach ihrem Wert in absteigender Reihenfolge.
        sortedEmotions = sortedEmotions.OrderByDescending(e => e.value).ToArray();

        // Begrenze die Anzahl der zu verwendenden Emotionen auf die verfügbare Anzahl.
        numberOfEmotionsToUse = Mathf.Clamp(numberOfEmotionsToUse, 1, sortedEmotions.Length);

        for (int i = 0; i < numberOfEmotionsToUse; i++)
        {
            EmotionType emotionType = sortedEmotions[i].emotionType;

            // Berechne den neuen Emotionswert nach dem Verbrauch.
            float newEmotionValue = GetEmotionValue(emotionType) - resourceCost;

            // Stelle sicher, dass der Emotionswert nicht unter 0 fällt.
            newEmotionValue = Mathf.Max(newEmotionValue, 0f);

            // Setze den neuen Emotionswert.
            SetEmotionValue(emotionType, newEmotionValue);
        }
    }


    public void DistributeEmotions()
    {
        EmotionType highestEmotion = EmotionType.Wut; // Start mit einer beliebigen Emotion
        float highestValue = GetEmotionValue(EmotionType.Wut); // Start mit dem Wert der "Wut"-Emotion

        foreach (EmotionType emotionType in Enum.GetValues(typeof(EmotionType)))
        {
            float value = GetEmotionValue(emotionType);
            if (value > highestValue)
            {
                highestValue = value;
                highestEmotion = emotionType;
            }
        }

        // Setze die höchste Emotion auf null und verteile ihren Wert auf die anderen Emotionen
        float redistributedValue = highestValue / (Enum.GetValues(typeof(EmotionType)).Length - 1); // -1, um die höchste Emotion auszuschließen
        SetEmotionValue(highestEmotion, 0f);

        foreach (EmotionType emotionType in Enum.GetValues(typeof(EmotionType)))
        {
            if (emotionType != highestEmotion)
            {
                float currentValue = GetEmotionValue(emotionType);
                SetEmotionValue(emotionType, currentValue + redistributedValue);
            }
        }
    }

    // Methode, um zu überprüfen, ob sich der Spieler in ein Monster verwandeln kann
    private void CheckMonsterTransformation()
    {
        int countAboveThreshold = 0;

        // Zähle, wie viele Emotionen den Schwellenwert überschreiten
        foreach (FloatValue floatValue in emotionValues)
        {
            if (floatValue.value >= monsterTransformationThreshold)
                countAboveThreshold++;
        }

        if (countAboveThreshold == 1)
        {
            NearlyMorbingTime?.Invoke(this, EventArgs.Empty);

        }
        // Wenn mindestens 2 Emotionen den Schwellenwert überschreiten, verwandelt sich der Spieler in ein Monster
        if (countAboveThreshold >= 2)
        {
            MonsterTransformation();
        }
    }

    public void UpdateEmotions(FloatValue[] newEmotionValues)
    {
        emotionValues = newEmotionValues;
        CheckMonsterTransformation();
    }

    // Methode zum Abrufen des Emotionswerts einer bestimmten Emotion
    public float GetEmotionValue(EmotionType emotionType)
    {
        int index = (int)emotionType;
        return emotionValues[index].value;
    }

    private void MonsterTransformation()
    {
        OhNoItsMorbingTime.Invoke(this, EventArgs.Empty);
    }


    private void SetEmotionValue(EmotionType emotionType, float newValue)
    {
        int index = (int)(emotionType);
        emotionValues[index].value = Mathf.Clamp(newValue, minEmotionValue, maxEmotionValue);

        //hier können sie auch ereignisse auslösen oder ander Aktionen auführen 
        OnEmotionValueChanged?.Invoke(this, new EmotionChangedEventArgs
        {
            emotionType = emotionType,
            newValue = emotionValues[index].value,
        });
    }

    //erhöht die verwandlungsthreshold zur monster verwandelung für eine bestimmte Zeit
    public void SetMonsterTransformationThreshhold(float monsterduration, float duration)
    {
        monsterTransformationThreshold += monsterduration;

        StartCoroutine(ResetThreshHold(duration));
    }

    public void NewAbsorbtionRate(float newAbsorbtionRate, float duration)
    {
        absorptionMultiplier = newAbsorbtionRate;
        StartCoroutine(ResetAbsorbtionRate(duration));
    }

    IEnumerator ResetAbsorbtionRate(float duration)
    {
        yield return new WaitForSeconds(duration);
        absorptionMultiplier = 1f;
    }

    public void SetMaxEmoitionValue(float maxEmoitionValueNew, float duration)
    {
        maxEmotionValue = maxEmoitionValueNew;
        maxEmotionValueChange?.Invoke(this, EventArgs.Empty);
        StartCoroutine(ResetThreshHold(duration));
    }

    IEnumerator ResetThreshHold(float duration)
    {
        yield return new WaitForSeconds(duration);
        monsterTransformationThreshold = 75f;
        maxEmotionValue = 100f;
        maxEmotionValueChange?.Invoke(this, EventArgs.Empty);
    }

    public void ConsumEmotionOverTime(float resourceCost, float consumptionIntervall)
    {
        if(consumeEmotionCoroutine == null)
        {
          consumeEmotionCoroutine = StartCoroutine(StartConsumptionOfEmotion(resourceCost, consumptionIntervall));
        }
    }

    IEnumerator StartConsumptionOfEmotion(float resourceCost, float consumptionIntervall)
    {
        while(true)
        {
            ConsumeEmotionAsResources(1, resourceCost);
            yield return new WaitForSeconds(consumptionIntervall);
        }
    }

    public void StopConsumptionOfEmotions(float rescourceCost, float consumptionIntervall)
    {
        if(consumeEmotionCoroutine != null)
        {
            StopCoroutine(consumeEmotionCoroutine);
            consumeEmotionCoroutine = null;
            Debug.Log("true");
        }
    }
}


