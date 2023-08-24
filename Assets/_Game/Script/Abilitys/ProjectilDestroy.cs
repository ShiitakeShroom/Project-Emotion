using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feind"))
        {
            Destroy(gameObject);
        }
    }
}
