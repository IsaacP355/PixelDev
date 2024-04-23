using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; //how fast the player can walk

    Vector2 moveInput;

    public bool IsMoving { get; private set; }

    Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start() { //will only be called once


    }

    // Update is called once per frame
    void Update() {


    }

    private void FixedUpdate() {
        body.velocity = new Vector2(moveInput.x * walkSpeed, body.velocity.y);

    }

    public void OnMove(InputAction.CallbackContext context) {

        moveInput = context.ReadValue<Vector2>(); //x and y inputs

        IsMoving = moveInput != Vector2.zero; //a  true/false statement


    }
}
