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

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float direction = isFacingRight ? 1.0f : -1.0f;

        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        //if (hit.collider == null)
        //{
        //    isAtEdge = true;
        //}
        //else
        //{
        //    isAtEdge = false;
        //}

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
                // Spawn the part prefab at the enemy's position
                Instantiate(partPrefab, transform.position, Quaternion.identity);
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                // Destroy the enemy
                Destroy(gameObject);

                // Destroy the bullet
                Destroy(collision.gameObject);
            }
        }
    }
}
