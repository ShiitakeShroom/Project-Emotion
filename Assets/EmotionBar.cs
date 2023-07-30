using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBar : MonoBehaviour
{
    public EmotionSystem emotionSystem;
    public Slider emotionSlider;
    //The Array of Emotions 
    public EmotionSystem.EmotionType emotionTypeToDisplay;

    public void Awake()
    {
        emotionSystem = FindObjectOfType<EmotionSystem>();
    }

    public void Start()
    {   //MaxValue für Emotion
        emotionSystem.OnEmotionValueChanged += OnEmotionValueChanged;
        SetMaxEmotion(emotionSystem.maxEmotionValue);
    }

    private void OnEmotionValueChanged(object sender, EmotionSystem.EmotionChangedEventArgs e)
    {
        if (e.emotionType == emotionTypeToDisplay)
        {
            SetEmotion(e.newValue);
        }
    }


    public void SetMaxEmotion(float maxEmotionValue)
    {

        emotionSlider.maxValue = maxEmotionValue;

    }

    //Set the current emotion to value of the Slider
    public void SetEmotion(float emotionValue)
    {
        emotionSlider.value = emotionValue;
    }
}
