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
            emotionValues[index].value += emotion.valueToAbsorb;

            // Den Emotionswert innerhalb der Grenzen halten;
            emotionValues[index].value = Mathf.Clamp(emotionValues[index].value, minEmotionValue, maxEmotionValue);


            if (emotion.valueToAbsorb > 0)
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
    public void ConsumeEmotionAsResources(EmotionType[] requierdEmotiones, float resourceCost)
    {
        foreach (EmotionType emotionType in requierdEmotiones)
        {
            float newEmotionValue = GetEmotionValue(emotionType) - resourceCost;

            newEmotionValue = Mathf.Max(newEmotionValue, 0f);

            SetEmotionValue(emotionType, newEmotionValue);
        }
    }

    public void DistributeEmotions()
    {
        // Finde die Emotion mit dem höchsten Wert
        EmotionType highestEmotion = EmotionType.Wut;
        float highestValue = GetEmotionValue(EmotionType.Wut);

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
        float redistributedValue = highestValue / 6f;
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

}


