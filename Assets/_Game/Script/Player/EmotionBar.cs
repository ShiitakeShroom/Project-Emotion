using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBar : MonoBehaviour
{
    public Slider emotionSlider;
    public Color belowThreshholdColor = Color.blue;
    public Color aboveThreshholdColor = Color.red;
    public EmotionSystem emotionSystem;
    //The Array of Emotions 
    public EmotionSystem.EmotionType emotionTypeToDisplay;

    public GameObject marker;

    public void Awake()
    {
        emotionSystem = GetComponent<EmotionSystem>();
        if (emotionSystem == null ) { 
            emotionSystem = FindObjectOfType<EmotionSystem>();
        }
    }

    public void Start()
    {   //MaxValue für Emotion
        emotionSystem.OnEmotionValueChanged += OnEmotionValueChanged;
        emotionSystem.maxEmotionValueChange += MaxEmotionValueChange;
        SetMaxEmotion(emotionSystem.maxEmotionValue);

        // Initialize the marker at 75% position.
        InitializeMarker();
    }
    //Event das automatische die Emotionbars aktualisiert
    private void OnEmotionValueChanged(object sender, EmotionSystem.EmotionChangedEventArgs e)
    {
        if (e.emotionType == emotionTypeToDisplay)
        {
            SetEmotion(e.newValue);
        }
    }

    //Set the current emotion to value of the Slider
    public void SetEmotion(float emotionValue)
    {
        emotionSlider.value = emotionValue;

        if (emotionSlider.value < emotionSystem.monsterTransformationThreshold)
        {
            emotionSlider.fillRect.GetComponent<Image>().color = belowThreshholdColor;
        }
        else
        {
            emotionSlider.fillRect.GetComponent<Image>().color = aboveThreshholdColor;
        }

        // Update the marker position.
        UpdateMarkerPosition();
    }

    public void UpdateEmotionBarFromSystem()
    {
        float updateEmotionValue = emotionSystem.GetEmotionValue(emotionTypeToDisplay);
        SetEmotion(updateEmotionValue);
    }
    public void SetMaxEmotion(float maxEmotionValue)
    {
        emotionSlider.maxValue = maxEmotionValue;
    }

    //soll die maxEmotionvalues des Sliders automatisch festlegen
    private void MaxEmotionValueChange(object sender, EventArgs e)
    {
        SetMaxEmotion(emotionSystem.maxEmotionValue);
    }


    // Function to initialize the marker at 75% position.
    private void InitializeMarker()
    {
        if (marker != null)
        {
            float markerPosition = emotionSlider.maxValue * emotionSystem.monsterTransformationThreshold/100;
            marker.transform.position = GetMarkerPosition(markerPosition);
        }
    }

    // Function to update the marker position based on the slider value.
    private void UpdateMarkerPosition()
    {
        if (marker != null)
        {
            float markerPosition = emotionSlider.maxValue * emotionSystem.monsterTransformationThreshold/100;
            marker.transform.position = GetMarkerPosition(markerPosition);
        }
    }

    // Function to calculate the marker's position on the slider.
    private Vector3 GetMarkerPosition(float position)
    {
        float sliderWidth = emotionSlider.GetComponent<RectTransform>().rect.width;
        float sliderMinX = emotionSlider.transform.position.x - sliderWidth / 2.0f;
        float sliderMaxX = emotionSlider.transform.position.x + sliderWidth / 2.0f;
        float normalizedPosition = position / emotionSlider.maxValue;
        float markerX = Mathf.Lerp(sliderMinX, sliderMaxX, normalizedPosition);

        return new Vector3(markerX, emotionSlider.transform.position.y, emotionSlider.transform.position.z);
    }
}
