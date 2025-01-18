using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bigCircle;
    public Transform smallCircle;
    public Transform middleCircle;

    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float impactForce = 10f;
    public float maxImpact = 4f;
    public float maxVelocityMagnitude = 5f;


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get input from arrow keys or W/A/S/D keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // Apply force to the rigidbody for movement
        rb.AddForce(movement * moveSpeed);

        // Prevent angular motion
        /*rb.angularVelocity = 0f;*/


    }

    private void LateUpdate()
    {
        float force = Mathf.InverseLerp(0f, maxVelocityMagnitude, rb.velocity.magnitude);

        // var scale = Vector2.one * Mathf.Clamp(force, 0.75f, 1f);
        //
        // bigCircle.localScale = Vector2.Lerp(bigCircle.localScale, scale, Time.deltaTime * 2f);
    }

    /*void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector2 moveDirection = transform.up * moveInput;

        rb.AddForce(moveDirection * moveSpeed);

        float turnAmount = -turnInput * turnSpeed * Time.deltaTime;

        rb.MoveRotation(rb.rotation + turnAmount);

        float velocityPower = Mathf.InverseLerp(0f, maxVelocityMagnitude, rb.velocity.magnitude);

        Debug.Log(rb.velocity.magnitude + " magnitude " + velocityPower);
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 pushDirection = contact.normal;

            float pushForce = Mathf.Clamp(rb.velocity.magnitude * impactForce, 0f, maxImpact);

            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
