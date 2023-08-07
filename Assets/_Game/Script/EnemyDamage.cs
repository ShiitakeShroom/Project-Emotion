using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public float damage = 50f;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

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
            Debug.Log("Enemy gets " + damage);
        }
    }
}
