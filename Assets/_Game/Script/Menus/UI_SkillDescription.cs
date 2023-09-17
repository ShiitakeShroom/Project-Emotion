using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillDescription : MonoBehaviour
{
    [SerializeField]
    private TMP_Text title;

    [SerializeField]
    private TMP_Text description;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(string skillName, string skillDescription)
    {
        this.title.text = skillName;
        this.description.text= skillDescription;
    }
}
