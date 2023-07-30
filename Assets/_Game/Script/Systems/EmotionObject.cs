using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionObject : MonoBehaviour
{

    public Player_Base playerBase;

    public EmotionSystem emotionSystem;

    public StatusManager statusManager;

    public int numberOfEmotions; //Anzahl der Emotione

    public EmotionData[] emotionData;//Emotionen und ihrer Wert

    public bool emotionAbsorbed = false;

    [System.Serializable]
    public struct EmotionData
    {
        public EmotionSystem.EmotionType emotionType;
        public float valueToAbsorb;
    }

    private void Awake()
    {
        statusManager = FindObjectOfType<StatusManager>();
        emotionSystem = FindObjectOfType<EmotionSystem>();
        playerBase = FindObjectOfType<Player_Base>();
    }

    //Also switch statment wenn man 1 emotion hat zwei emotionen hat oder drei emotioen hat 
    //Soll alles über den Gamehandler bestimmt werden


    public void AbsorbEmotionFromNPC()
    {
        if (!emotionAbsorbed)
        {
            for (int i = 0; i < numberOfEmotions; i++)
            {
                emotionSystem.AbsorbEmotion(new EmotionObject.EmotionData[] { emotionData[i] });
            }

            emotionAbsorbed = true;
        }
    }

    public void CharacterUpdateStatus()
    {
        foreach(EmotionSystem.EmotionType emotionType in System.Enum.GetValues(typeof(EmotionSystem.EmotionType)))
        {
            int index = (int)emotionType;
            statusManager.emotionValues[index] = emotionSystem.GetEmotionValue(emotionType);
        }
    }
}
