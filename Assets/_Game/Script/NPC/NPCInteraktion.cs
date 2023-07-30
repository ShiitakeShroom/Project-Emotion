using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPCInteraktion : MonoBehaviour
{

    public string interactionMessage = "Hallo! Mr.Morbintime";
    public string absorbedInteractionMessage = "mir gehts nicht so gut";

    public bool interactionEnabled = false;
    public bool alreadyInteracted = false;

    public EmotionObject emotionObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionEnabled = true;
            Debug.Log("Its in!!! AHHHHHHH");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionEnabled = false;
            Debug.Log("BUT IT BACK IN");
        }
    }


    private void Update()
    {
        if (interactionEnabled == true && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (alreadyInteracted == false && emotionObject.emotionAbsorbed == false)
        {
            Debug.Log(interactionMessage);
            emotionObject.AbsorbEmotionFromNPC();
            alreadyInteracted = true;
            return;
        }

        //Ein zweiter interaction "string" brauch einen neuen Funktion
        if(alreadyInteracted == true && emotionObject.emotionAbsorbed == true)
        {
            Debug.Log(absorbedInteractionMessage);
        }

    }
}