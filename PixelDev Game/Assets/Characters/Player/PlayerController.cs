using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; //how fast the player can walk
    public float jumpImpulse = 10f;

    Vector2 moveInput;
    TouchingDirections touchingDirections;

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get {
        return _isMoving;
    } private set {
        _isMoving = value;
        animator.SetBool("isMoving", value);
    } }
    public bool _isFacingRight = true;
    public bool IsFacingRight { get {return _isFacingRight;}private set {
        if(_isFacingRight != value ) {
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;
    } }
    Rigidbody2D body;
    Animator animator;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();

    }


    private void FixedUpdate() {
        body.velocity = new Vector2(moveInput.x * walkSpeed, body.velocity.y);

        animator.SetFloat("yVelocity", body.velocity.y);

    }

    public void OnMove(InputAction.CallbackContext context) {

        moveInput = context.ReadValue<Vector2>(); //x and y inputs

        IsMoving = moveInput != Vector2.zero; //a  true/false statement

        SetFaceDirection(moveInput);


    }

    private void SetFaceDirection(Vector2 moveInput)
    {
        if(moveInput.x>0&&!IsFacingRight)
        {
            IsFacingRight = true; //
        } else if (moveInput.x <0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("jump");
            body.velocity = new Vector2(body.velocity.x, jumpImpulse);
        }
    }
}
