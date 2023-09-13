using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionObject : MonoBehaviour
{
    public EmotionSystem emotionSystem;

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
        emotionSystem = FindObjectOfType<EmotionSystem>();
    }

    //Also switch statment wenn man 1 emotion hat zwei emotionen hat oder drei emotioen hat 
    //Soll alles über den Gamehandler bestimmt werden


    public void AbsorbEmotionFromNPC()
    {
        if (!emotionAbsorbed)
        {
            for (int i = 0; i < numberOfEmotions; i++)
            {
                EmotionData emotion = emotionData[i];
                emotionSystem.AbsorbEmotion(new EmotionObject.EmotionData[] { emotion});
            }
            emotionAbsorbed = true;
        }
    }
}
