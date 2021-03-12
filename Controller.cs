using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed = 3.0f;
    public float gravity = -9.8f;
    public float jump = 3f;

    private CharacterController myController;

    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Vector3 fall;

    void Start() {
        myController = gameObject.GetComponent<CharacterController>();
    }

    void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && fall.y < 0) {
            fall.y = -2f;
        }

        Vector3 movimentoZ = Input.GetAxis("Vertical") * Vector3.forward * speed * Time.deltaTime;
        Vector3 movimentoX = Input.GetAxis("Horizontal") * Vector3.right * speed * Time.deltaTime;

        Vector3 movimento = transform.TransformDirection(movimentoZ + movimentoX);

        if (isGrounded && Input.GetButtonDown("Jump")) {
            fall.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        fall.y += gravity * Time.deltaTime;

        myController.Move(movimento);
        myController.Move(fall * Time.deltaTime);
    }
}
