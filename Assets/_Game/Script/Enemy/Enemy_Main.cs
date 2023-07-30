using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Main : MonoBehaviour
{

    public _Enemy Enemy { get; private set; }
    public HealthSystem healthSystem { get; private set; }


    public void Awake()
    {
        Enemy = GetComponent<_Enemy>();
        healthSystem = GetComponent<HealthSystem>();
        healthSystem = new HealthSystem(100);
    }
}
