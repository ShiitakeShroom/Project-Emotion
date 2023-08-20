using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchtsamkeitsMenu : MonoBehaviour
{
    public static bool playerLooksAfterEmotion = false;
    public CanvasGroup mindfulnessMenuUI;
    public List<EmotionBar> emotionBars;

    public float timer = 2f;
    public float elapsedTime = 0.0f;
    public float slowMotion;

    public void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMindFullness();
        }

        if (playerLooksAfterEmotion)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timer)
            {
                ResumeGame();
            }
        }
    }

    void ToggleMindFullness()
    {
        if (playerLooksAfterEmotion)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        mindfulnessMenuUI.alpha = 1f;
        playerLooksAfterEmotion = true;
        Time.timeScale = slowMotion; //slow time

        foreach (EmotionBar emotionBar in emotionBars)
        {
            emotionBar.UpdateEmotionBarFromSystem();
        }
    }

    void ResumeGame()
    {
        playerLooksAfterEmotion = false;
        mindfulnessMenuUI.alpha = 0f;
        Time.timeScale = 1.0f; //ResumeTime
        elapsedTime = 0.0f;

        foreach (EmotionBar emotionBar in emotionBars)
        {
            emotionBar.UpdateEmotionBarFromSystem();
        }
    }

}
