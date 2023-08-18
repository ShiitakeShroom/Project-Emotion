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

public class EmotionSystem: MonoBehaviour
{ 
    [System.Serializable]
    public class FloatValue
    {
        public EmotionType emotionType;
        public float value;
    }
    //Define the Event, whenever a emotion will be changed
    public event EventHandler<EmotionChangedEventArgs> OnEmotionValueChanged;

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


    // Methode zum Absorbieren einer Emotion von einem NPC oder Gegner

    //public void AbsorbEmotion(EmotionType emotionType, float value)
    public void AbsorbEmotion(EmotionObject.EmotionData[] emotions)
    {
        foreach(EmotionObject.EmotionData emotion in emotions)
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

        if(countAboveThreshold == 1)
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
}


