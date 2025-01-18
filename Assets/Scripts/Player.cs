using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public float impactForce = 10f;
    public float maxImpact = 4f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector2 moveDirection = transform.up * moveInput;

        rb.AddForce(moveDirection * moveSpeed);

        float turnAmount = -turnInput * turnSpeed * Time.deltaTime;

        rb.MoveRotation(rb.rotation + turnAmount);
    }

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
