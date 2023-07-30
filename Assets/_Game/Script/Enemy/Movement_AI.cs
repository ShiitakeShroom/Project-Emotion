using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_AI : MonoBehaviour
{
    public Transform target;
    //public Animator animator;
    public float speed = 0f;

    Vector3 playerPos, enemyPos;

    private void Update()
    {
        if(target != null)
        {
            playerPos = new Vector3(target.localPosition.x, target.localPosition.y, target.localPosition.z);
            enemyPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);

            if(Vector3.Distance(transform.transform.position, target.transform.position) > 1.3)
            {
                transform.position = Vector3.MoveTowards(enemyPos, playerPos, 2 * Time.deltaTime);
            }
            if(Vector3.Distance(transform.transform.position, target.transform.position) < 1.15)
            {
                transform.position = Vector3.MoveTowards(enemyPos, playerPos, -1 * Time.deltaTime);
            }
        }
    }
}
