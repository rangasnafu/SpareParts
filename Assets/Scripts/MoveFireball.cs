using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFireball : MonoBehaviour
{
    public Vector2 movementSpeed;
    public Space space;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        PlayerController playerController = playerObject.GetComponent<PlayerController>();

        movementSpeed = playerController.isFacingRight ? movementSpeed : -movementSpeed;
        // assuming isFacingRight is true but if not, negative movementSpeed (: is short hand if)
    }

    // Update is called once per frame
    void Update()
    {
        //move the bullet 
        transform.Translate(movementSpeed * Time.deltaTime, space);
    }

    // Destroy the bullet when it collides with the Bullet Destroyer
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletDestroyer"))
        {
            Debug.Log("Bullet collided with Bullet Destroyer");
            Destroy(gameObject);
        }
    }
}
