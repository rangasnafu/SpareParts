using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MoveFireball : MonoBehaviour
{
    public Vector2 movementSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject.TryGetComponent<PlayerController>(out var playerController))
        {
            movementSpeed = playerController.isFacingRight ? movementSpeed : -movementSpeed;
            spriteRenderer.flipX = !playerController.isFacingRight;
        }
        rb.AddForce(movementSpeed);
        // assuming isFacingRight is true but if not, negative movementSpeed (: is short hand if)
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //move the bullet 
    //    // transform.Translate(movementSpeed * Time.deltaTime, space);
    //    rb.velocity = new Vector2(movementSpeed.x, rb.velocity.y);
    //}

    // Destroy the bullet when it collides with the Bullet Destroyer
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletDestroyer"))
        {
            Debug.Log("Bullet collided with Bullet Destroyer");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bye;");
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
