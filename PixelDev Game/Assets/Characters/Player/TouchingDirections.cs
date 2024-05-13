using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D body;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    //Rigidbody2D body;
    [SerializeField]
    private bool _isGrounded;


    public bool IsGrounded { get {
        return _isGrounded;
    } private set {
        _isGrounded = value;
        animator.SetBool("isGrounded", value);
    } }


    private void Awake ()
    {
        body = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    
  
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }
}
