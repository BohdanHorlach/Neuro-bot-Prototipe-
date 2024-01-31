using System;
using UnityEngine;

class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private Movement movementPlayer;
    [SerializeField] private Animator animator;
    [SerializeField] private KeyCode sprutKeyCode;

    private float moveX;
    private float moveY;
    private float currentY;
    private float pastY;

    private void OnEnable()
    {
        currentY = transform.position.y;
        pastY = currentY;
    }


    private void Update()
    {
        GetValue();
        SetValue();
    }


    private void GetValue()
    {
        pastY = currentY;
        moveX = Input.GetAxis("Horizontal");
        currentY = (float)Math.Round(transform.position.y, 2);
        moveY = currentY - pastY;
    }


    private void SetValue()
    {
        if (Input.GetButtonDown("Jump") && movementPlayer.HasDoubleJump != false) {
            animator.SetTrigger("isJump"); 
        }

        if (Input.GetKeyDown(sprutKeyCode)) {
            animator.SetBool("isSprut", true);
        }


        animator.SetFloat("moveX", Math.Abs(moveX));
        animator.SetFloat("moveY", moveY);
    }


    private void ResetSprut()
    {
        animator.SetBool("isSprut", false);
    }
}