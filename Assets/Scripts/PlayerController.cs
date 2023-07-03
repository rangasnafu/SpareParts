using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;

    public bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float shootInterval;
    private float shootTimer;

    public float deathDelay;

    private bool canInteract = false;
    private bool isInteracting = false;

    private Parts partsUI;

    // Start is called before the first frame update
    private void Start()
    {
        partsUI = FindObjectOfType<Parts>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (IsGrounded() && Mathf.Abs(horizontal) > 0f)
        {
            //animator.SetBool("isRunning", true);
        }
        else
        {
            //animator.SetBool("isRunning", false);
        }

        UpdateFireballShot();

        if (Input.GetKeyDown(KeyCode.E) && canInteract && !isInteracting)
        {
            isInteracting = true;
            ResetParts();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void UpdateFireballShot()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Mouse0))
        {
            shootTimer = shootInterval;
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        //instantiate means to spawn, quaternion means rotation
    }

    private int parts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Part"))
        {
            parts += 1; // Increment the parts by 1
            Destroy(collision.gameObject); // Destroy the part prefab
            UpdatePartsUI(); // Update the parts UI display
        }
        else if (collision.CompareTag("Merchant"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Merchant"))
        {
            canInteract = false;
        }
    }

    private void ResetParts()
    {
        parts = 0; // Set the parts value to 0
        UpdatePartsUI(); // Update the parts UI display
        isInteracting = false; // Reset the interaction flag

        if (partsUI != null)
        {
            partsUI.UpdateMoneyDisplay(30); // Set the money value to 30 in the UI
        }
    }

    private void UpdatePartsUI()
    {
        Parts partsUI = FindObjectOfType<Parts>();
        if (partsUI != null)
        {
            partsUI.UpdatePartsDisplay(parts); 
        }
    }
}
