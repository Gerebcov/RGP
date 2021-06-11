using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameTrigger isGroundedTrigger;


    private void Update()
    {
        animator.SetFloat("velocity.x", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocity.y", rb.velocity.y);
        animator.SetBool("isGrounded", isGroundedTrigger.Enter);
        animator.SetBool("isMoved", rb.velocity.x != 0);
    }

}
