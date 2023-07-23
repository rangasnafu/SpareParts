using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform playerDetection;
    public float shootTimer;
    public float bulletSpeed;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float direction = isFacingRight ? 1.0f : -1.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (shootTimer <= 0f)
            {
                ShootBullet();
                shootTimer = 2f; // Reset the shoot timer
            }
        }
    }

    private void ShootBullet()
    {
        Vector2 shootDirection = isFacingRight ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
    }

}
