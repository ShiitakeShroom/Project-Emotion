using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilPattern : MonoBehaviour
{
    public float damage = 20f;
    public float delayAfter = 1f;
    public float speed = 10f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delayAfter);

        rb = GetComponent<Rigidbody>();
        Vector3 directon = transform.forward;
        Vector3 velocity = directon * speed;
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feind"))
        {
            Debug.Log("Hit it");
            other.GetComponent<EnemyHealth>().DecreaseHealth(damage);
        }
    }
}
