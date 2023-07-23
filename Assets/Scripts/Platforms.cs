using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    private Collider2D platformCollider;
    private Collider2D playerCollider;

    private void Start()
    {
        // Get the platform's collider component
        platformCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's collider component
            playerCollider = collision.collider;

            // Disable collision between the player and the platform from underneath
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Re-enable collision between the player and the platform from underneath
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
}
