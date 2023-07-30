using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{   
    public Animator animator;
    public OverworldControllerPlayer player;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalInput, 0f, verticalInput).normalized;

        //Aktualisierd die Parameter des Animators
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.z);

        //Aktivirt die Animation basiernd auf der Richtung

        if(direction != Vector3.zero) 
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
