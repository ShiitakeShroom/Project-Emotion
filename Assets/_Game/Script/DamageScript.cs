using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{

    public StatusManager status;
    public int damage = 50;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        DoDamage(); 
    }

    public void DoDamage()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            status.TakeDamage(damage);
        }
    }
}
