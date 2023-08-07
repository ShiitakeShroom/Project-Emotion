using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public float damage = 50f;
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
            playerHealth.DecreaseHealth(damage);
        }
    }
}
