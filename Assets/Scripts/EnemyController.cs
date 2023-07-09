using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public float detectionRadius = 5f;
    public float moveSpeed = 3f;
    //private bool playerDetected = false;

    private Transform player;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;

    //private bool isGrounded = false;

    public GameObject partPrefab;

    private void Update()
    {
        //if (!playerDetected)
        //{
        //    float distance = Vector2.Distance(transform.position, player.position);
         //   if (distance < detectionRadius)
        //    {
        //        playerDetected = true;
        //    }
        //}

        //if (playerDetected)
        //{
            // Check if the enemy is grounded
        //    isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            // Move towards the player or perform any other actions
        //    Vector2 direction = player.position - transform.position;
        //    rb.velocity = new Vector2(direction.normalized.x * moveSpeed, rb.velocity.y);

            // Flip the enemy sprite based on the movement direction
        //    if (direction.x < 0)
        //        transform.localScale = new Vector3(-1, 1, 1);
         //   else if (direction.x > 0)
         //       transform.localScale = new Vector3(1, 1, 1);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            // Spawn the part prefab at the enemy's position
            Instantiate(partPrefab, transform.position, Quaternion.identity);

            // Destroy the enemy
            Destroy(gameObject);

            // Destroy the bullet
            Destroy(collision.gameObject);
        }
    }
}
