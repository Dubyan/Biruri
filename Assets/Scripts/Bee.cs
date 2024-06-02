using System.Collections;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public float speed = 5f;
    public PlayerMovement player;

    private void Update()
    {

            transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(3);
            }
        }
    }
}