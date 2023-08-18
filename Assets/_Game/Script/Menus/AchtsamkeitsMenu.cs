using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchtsamkeitsMenu : MonoBehaviour
{
    public static bool playerLooksAfterEmotion = false;
    public GameObject mindfulnessMenuUI;
    public EmotionBar[] emotionBars;

    public float timer = 2f;
    public float elapsedTime = 0.0f;
    public float slowMotion;

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

    void ResumeGame()
    {

        playerLooksAfterEmotion = false;
        mindfulnessMenuUI.SetActive(false);
        Time.timeScale = 1.0f; //ResumeTime
        elapsedTime = 0.0f;
    }

    void PauseGame()
    {
        playerLooksAfterEmotion = true;
        mindfulnessMenuUI.SetActive(true);
        Time.timeScale = slowMotion; //slow time

        foreach(EmotionBar emotionBars in emotionBars)
        {
            emotionBars.UpdateEmotionBarFromSystem();
        }
    }
}
