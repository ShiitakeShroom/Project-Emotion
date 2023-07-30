using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Funktioniert; nicht anfassen !!!!!!!!!!
    public static CameraFollow Instance { get; private set; }

    [SerializeField] private Transform playerTransform; // Das Zielobjekt, dem die Kamera folgt
    [SerializeField] private Vector3 cameraOffset; // Der Offset zwischen der Kamera und dem Zielobjekt
    [SerializeField] private float smoothSpeed = 0.125f; // Die Geschwindigkeit, mit der die Kamera dem Zielobjekt folgt


    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        if(playerTransform != null) 
        { 
            Vector3 desiredPosition = playerTransform.position + cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(playerTransform);
        }
        else { 
            // noch irgendwann in einen State umbauen 
            GameStop();
        }
    }

    private void GameStop()
    {
        if(playerTransform == null)
        {
            Application.Quit();
        }
    }
}

