using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    public float pushForce = 10f;
    public float raycastDistance = 0.1f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isCollidingWithWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckVerticalCollision();

        if (Input.GetKeyDown(KeyCode.Space) && !isCollidingWithWall)
        {
            Jump();
        }
    }

    void CheckVerticalCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance);
        if (hit.collider != null)
        {
            Vector2 pushDirection = Vector2.down;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            isCollidingWithWall = true;
            Invoke("ResetWallCollision", 0.5f); // Время, через которое включится возможность прыжка снова
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void ResetWallCollision()
    {
        isCollidingWithWall = false;
    }
}