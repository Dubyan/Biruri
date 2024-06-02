using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isWorm;
    private int currentHealth;
    public int maxHealth = 2;
    public float speed = 3f;
    public float maxDistanceLeft = 3.5f;
    private SpriteRenderer spriteRenderer;
    public float maxDistanceRight = 3.5f;
    private int moveDirection = 1; // 1 - вправо, -1 - влево
    private Animator anim;
    private Vector3 spawnPosition;
    private enum StateMouse { run, fight, die }
    private bool inTrigger = false;
    private bool isInvulnerable = false;
    private float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool canTakeDamage = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawnPosition = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;

    }
    private StateMouse State
    {
        get { return (StateMouse)anim.GetInteger("state"); }
        set
        {
            int newStateValue = (int)value;
            if (isWorm)
            {
                newStateValue += 10;
            }
            anim.SetInteger("state", newStateValue);
        }
    }



    void Update()
    {
        CheckPlayerInRange();
        Fight();
        Move();
        if (canTakeDamage)
        {
            TakeDamage(1);
        }
        if (currentHealth <= 0)
        {
            inTrigger = false;
            Die();
        }
    }

    void CheckPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.transform.position.x >= spawnPosition.x - maxDistanceLeft && player.transform.position.x <= spawnPosition.x + maxDistanceRight)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    moveDirection = -1; 
                }
                else
                {
                    moveDirection = 1; 
                }
            }
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(speed * moveDirection * Time.deltaTime, 0, 0);
        transform.Translate(movement);
        if (moveDirection == -1)
        {
            Flip();
            MoveRange();
        }
        else if (moveDirection == 1)
        {
            Flip();
            MoveRange();
        }
    }

    void MoveRange()
    {
        Vector3 movement = new Vector3(speed * moveDirection * Time.deltaTime, 0, 0);
        transform.Translate(movement);
        if (transform.position.x - spawnPosition.x > maxDistanceRight)
        {
            moveDirection = -1;
            Flip();
            State = StateMouse.run;
        }
        else if (spawnPosition.x - transform.position.x > maxDistanceLeft)
        {
            moveDirection = 1;
            State = StateMouse.run;
            Flip();
        }
    }
    void Fight()
    {
        if(inTrigger)
        {
            State = StateMouse.fight;
        }
    }

    void Flip()
    {
        if (moveDirection == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
        else if (moveDirection == 1)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCanTakeDamage(bool canTake)
    {
        canTakeDamage = canTake;
    }

    public bool CanTakeDamage()
    {
        return canTakeDamage;
    }

    public void TakeDamage(int damageAmount)
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.S) && canTakeDamage)
        {
            StartCoroutine(DelayedDamage(damageAmount));
        }
    }

    IEnumerator DelayedDamage(int damageAmount)
    {
        SetCanTakeDamage(false);
        yield return new WaitForSeconds(0.3f);

        currentHealth -= damageAmount;
        StartCoroutine(FlashSpriteRenderer());
        StartCoroutine(Knockback());

        SetCanTakeDamage(true);
    }

    IEnumerator Knockback()
    {
        float knockbackDuration = 0.7f;
        float knockbackForce = 5f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = transform.position - player.transform.position;
            direction.Normalize();

            rb.velocity = new Vector2(direction.x * knockbackForce, jumpForce);

            float timer = 0;
            while (timer < knockbackDuration)
            {
                timer += Time.deltaTime;
                if(currentHealth>0) rb.velocity = new Vector2(direction.x * knockbackForce, 4 * jumpForce * (knockbackDuration - timer * 2));
                yield return null;
            }
        }
    }

    void Die()
    {
        State = StateMouse.die; 
        StartCoroutine(DisappearAfterDelay(1f));
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }
    }

    IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        Destroy(gameObject);
    }

    IEnumerator FlashSpriteRenderer()
    {
        float duration = 0.6f;
        float interval = 0.01f;
        SetInvulnerable(true);
        while (duration > 0)
        {

            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
            duration -= interval;
        }
        SetInvulnerable(false);
        spriteRenderer.enabled = true;
    }
}