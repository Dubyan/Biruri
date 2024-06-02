using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    public AudioSource Sound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        MoveRight();
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        Sound.Play();
    }

    void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }


    public bool CheckDestination()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (Collider2D col in colliders)
        {
            if (col.isTrigger)
            {
                return true;
            }
        }
        return false;
    }
}