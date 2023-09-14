using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionBehavoir : MonoBehaviour
{
    public Skill_JustSayHallo justSayHello;
    public float timeTillDelet;

    private void Start()
    {
        timeTillDelet = justSayHello.duration;

        DestroyCompanion();
    }


    public void DestroyCompanion()
    {
        StartCoroutine(DestroyIt());
    }
     
    IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(timeTillDelet);
        Destroy(this.gameObject);
    }
}
