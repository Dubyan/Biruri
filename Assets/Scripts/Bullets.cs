using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerMovement player;
    public float knockbackForce = 10f;
    public int damage = 1;
    private DestroyOnInput box;

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            player = other.GetComponent<PlayerMovement>();
            if (player != null && !player.IsInvulnerable())
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // ”ничтожаем пулю после попадани€ в игрока
        }
        else if(other.CompareTag("Box"))
        {
            box = other.GetComponent<DestroyOnInput>();
            box.Destroying();
            Destroy(gameObject);
        }
    }
}