using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical"); // W/S or Arrow Up/Down
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Arrow Left/Right

        Vector2 moveDirection = transform.up * (moveInput * moveSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + moveDirection);

        float turnAmount = -turnInput * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation + turnAmount);
    }

}
