using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMiniGame : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public bool secGame;
    private Vector2 startPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        transform.position += movement * speed * Time.deltaTime;
    }

    public bool CheckDestination()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Destination"))
            {
                return true;
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (secGame && !other.CompareTag("Destination"))
        {
            transform.position = startPosition;
        }
    }
}