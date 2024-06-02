using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : MonoBehaviour
{
    private bool inTrigger = false;
    private PlayerMovement player;
    public float time = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            player = other.GetComponent<PlayerMovement>();
            if (player != null && !player.IsInvulnerable())
            {
                StartCoroutine(DelayedDamage(player, time)); // Устанавливаем задержку в 3 секунды
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    private IEnumerator DelayedDamage(PlayerMovement player, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (inTrigger)
        {
            player.TakeDamage(1);
            player.SetInvulnerable(true);
            yield return new WaitForSeconds(3f); // Устанавливаем время невосприимчивости после урона
            player.SetInvulnerable(false);
        }
    }
}