using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamge : MonoBehaviour
{

    public EnemyHealth enemyHealth;
    public float damage = 50f;
    // Start is called before the first frame update

    public void Start()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
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
            enemyHealth.DecreaseHealth(damage);
            Debug.Log("Enemy gets " + damage);
        }
    }
}
