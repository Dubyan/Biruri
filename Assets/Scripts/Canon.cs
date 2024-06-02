using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 5f;
    public float fireRate = 1f;
    public float fireRange = 5f;
    public PlayerMovement Player;
    private float nextFireTime;
    public AudioSource Sound;
    private bool inTrigger = false;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            FireProjectile();

            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void FireProjectile()
    {
        if (inTrigger) Sound.Play();
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.right * projectileSpeed;
        Destroy(projectile, fireRange / projectileSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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
}