using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public InteractBox interactBox;
    public List<Animator> animators;
    private Vector3 inputDirection;
    public Rigidbody2D rb;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal,vertical,0);

        AnimateMovement(inputDirection);
        interactBox.UpdatePosition(inputDirection);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector3(horizontal, vertical,0).normalized;

        if (inputDirection != Vector3.zero)
        {
            rb.MovePosition(transform.position + inputDirection*speed*Time.deltaTime);
        }
    }

    void AnimateMovement(Vector3 direction)
    {
        if (animators != null && animators.Count > 0)
        {
            foreach (Animator animator in animators)
            {
                if (animator != null)
                {
                    if (direction.magnitude > 0)
                    {
                        animator.SetBool("isMoving", true);
                        animator.SetFloat("horizontal", direction.x);
                        animator.SetFloat("vertical", direction.y);
                    }
                    else
                    {
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
    }
}
