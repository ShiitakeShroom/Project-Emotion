using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feind"))
        {
            Debug.Log("hit me harder");
            other.GetComponent<EnemyHealth>().DecreaseHealth(damage);
        }
    }
}
