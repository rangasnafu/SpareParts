using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public float detectionRadius = 5f;
    public float moveSpeed = 2.0f;
    public float moveDistance = 2.0f;
    public bool isFacingRight = true;
    private bool isAtEdge = false;

    private Vector2 startingPosition;
    //private bool playerDetected = false;

    private Transform player;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;


    //private bool isGrounded = false;

    public GameObject partPrefab;

    public GameObject explosionEffect;

    private bool spawnedPart = false;

    //public Transform playerDetection;
    //public float shootTimer;
    //public float bulletSpeed;
    //public Transform bulletSpawnPoint;
    //public GameObject bulletPrefab;

    public Animator animator;
    //public AudioClip enemyDeathAudio;
    //public float deathDelay = 2.0f;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float direction = isFacingRight ? 1.0f : -1.0f;

        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);

        if (Mathf.Abs(transform.position.x - startingPosition.x) >= moveDistance || isAtEdge)
        {
            isFacingRight = !isFacingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!spawnedPart)
        {
            if (collision.gameObject.CompareTag("Fireball"))
            {
                spawnedPart = true;
                if (partPrefab == null)
                {
                    return;
                }
                // Spawn the part prefab at the enemy's position
                Instantiate(partPrefab, transform.position, Quaternion.identity);
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                // Destroy the enemy
                Destroy(gameObject);

                // Destroy the bullet
                Destroy(collision.gameObject);

                //AudioSource audioSource = GetComponent<AudioSource>();
                //audioSource.PlayOneShot(enemyDeathAudio);

                moveSpeed = 0.0f;
            }
        }

        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    moveSpeed = 0.0f;
        //}

        //if (collision.CompareTag("Player"))
        //{
         //   if (shootTimer <= 0f)
        //    {
        //        ShootBullet();
        //        shootTimer = 2f; // Reset the shoot timer
        //    }
        //}
    }

    //private void ShootBullet()
    //{
    //    Vector2 shootDirection = isFacingRight ? Vector2.right : Vector2.left;
    //    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    //    bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
    //}
}
