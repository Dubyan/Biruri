using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private int currentTask;
    public int maxHealth = 3;
    private int maxKitiCat = 3;
    public int currentKitiCat = 0;
    public int currentHealth;
    public float speed = 5f;
    public float jumpForce = 10f;
    bool isGrounded;
    private Rigidbody2D rb;
    private Animator anim;
    private bool doubleJump = false;
    private bool secJump = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D[] groundColliders;
    private float idleTimer = 0f;
    private const float idleTimeThreshold = 5f;
    private bool isInvulnerable = false;
    private ProgressController progressController;
    public AudioSource[] Sound;
    private int OldHealth;
    private int OldKitiCat;

    public void SetInvulnerable(bool invulnerable)
    {
        isInvulnerable = invulnerable;
    }

    public void SetTask(int task)
    {
        currentTask = task;
    }

    public int GetCurrentTask()
    {
        return currentTask;
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Start()
    {
        currentTask = 0;
        progressController = GameObject.FindGameObjectWithTag("progresscontroller").GetComponent<ProgressController>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = progressController.GetHealth();
        OldHealth = progressController.GetHealth();
        currentKitiCat = progressController.GetKitiCat();
        OldKitiCat = progressController.GetKitiCat();
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set
        {
            int newStateValue = (int)value;
            if (doubleJump)
            {
                newStateValue += 10;
            }
            anim.SetInteger("state", newStateValue);
        }
    }

    void Update()
    {
        Meows();
        CheckGrounded();
        Move();
        Jump();
        Fight();
        Activate();
        Sleeping();
        progressController.SetHealth(currentHealth);
        progressController.SetKitiCat(currentKitiCat);
        if (currentHealth <= 0)
        {
            progressController.SetHealth(OldHealth);
            progressController.SetKitiCat(OldKitiCat);
            Die();
        }

    }
    private int meows = 0;
    void Meows()
    {

        if (!Sound[6].isPlaying && !Sound[7].isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Q) && meows%2 == 0)
            { 
                Sound[6].Play();
                meows += 1;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && meows%2 ==1)
            {
                Sound[7].Play();
                meows += 1;
            }
        }
    }

    void Sleeping()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTimeThreshold)
            {
                State = States.sleep;
                if (!Sound[1].isPlaying)
                {
                    Sound[1].Play();
                }
            }
        }
        else
        {
            Sound[1].Stop();
            idleTimer = 0f;
        }
    }

    void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        List<Collider2D> validColliders = new List<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            if (!col.isTrigger)
            {
                validColliders.Add(col);
            }
        }

        isGrounded = validColliders.Count > 1;
    }

    void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (move > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (move < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (Mathf.Abs(move) > 0 && isGrounded)
        {
            State = States.run;
            if (!Sound[0].isPlaying)
            {
                Sound[0].Play();
            }
        }
        else
        {
            Sound[0].Stop();
            State = States.idle;
        }
    }

    void Fight()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            State = States.fight;
            if(!Sound[5].isPlaying) Sound[5].Play();
        }
    }


    void Activate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            State = States.activate;
            if (!Sound[4].isPlaying) Sound[4].Play();
        }
    }
    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            secJump = true;
            Sound[3].Play();
        }
        if (!isGrounded)
        {
            State = States.jump;
            if (doubleJump && secJump && Input.GetKeyDown(KeyCode.W))
            {
                State = States.secJump;
                Sound[3].Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                secJump = false;
            }
        }
    }


    void Die()
    {
        if (currentHealth <= 0)
        {
            RestartLevel();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Sound[2].Play();
        StartCoroutine(FlashSpriteRenderer());
        StartCoroutine(Knockback());
    }

    IEnumerator Knockback()
    {
        float knockbackDuration = 0.5f;
        float knockbackForce = 5f;
        float knockbackDirection = (spriteRenderer.flipX) ? 1f : -1f; // Определяем направление отбрасывания
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        float timer = 0;
        while (knockbackDuration >= timer)
        {
            timer += Time.deltaTime;
            rb.velocity = new Vector2(knockbackDirection * knockbackForce, 4*jumpForce*(knockbackDuration-timer*2));
            yield return null;
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            currentKitiCat -= 1;
        }
    }

    public void TakeKitiCat()
    {
        if(currentKitiCat < maxKitiCat) currentKitiCat += 1;
    }


    IEnumerator FlashSpriteRenderer()
    {
        float duration = 3f;
        float interval = 0.2f;

        while (duration > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
            duration -= interval;
        }
        spriteRenderer.enabled = true;
    }

    public void SetDoubleJump(bool dJump)
    {
        doubleJump = dJump;
    }


    public int GetCurrentKitiCat()
    {
        return currentKitiCat;
    }
}

public enum States { idle, run, jump, fight, fall, sleep, activate, secJump}