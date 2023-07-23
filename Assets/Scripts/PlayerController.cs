using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float acceleration = 0.1f;
    public float jumpingPower = 16f;

    public bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float shootInterval;
    private float shootTimer;

    public float deathDelay = 2f;

    private bool canInteract = false;
    public bool isInteracting = false;
    private bool isShowingObject = false;

    public Parts partsUI;
    private UpgradeMenuManager upgradeMenuManager;

    public GameObject shopPromptText;
    public GameObject fireTutorial;
    public GameObject catPrefab;

    public int catsOwned = 0;

    public Sprite catSprite;

    public GameObject objectPreview;

    public Transform objectSpawnPointRight;
    public Transform objectSpawnPointLeft;

    public TextMeshProUGUI catText;
    public GameObject catUI;

    public TextMeshProUGUI wizardCount;
    public int wizardsCollected = 0;

    public PlayerSoundManager soundManager;

    private bool canShootFireball = false;

    public SpriteRenderer fireballKey;
    public SpriteRenderer fireballIcon;

    // Start is called before the first frame update
    private void Start()
    {
        partsUI = FindObjectOfType<Parts>();
        upgradeMenuManager = FindObjectOfType<UpgradeMenuManager>();
        shopPromptText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() && !isInteracting)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            // clamp max negative velocity to -jumpingPower
            if (rb.velocity.y < -jumpingPower)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
            }

            soundManager.PlayerJumpSound();
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f && !isInteracting)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            soundManager.PlayerJumpSound();
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
            //ResetParts();
            ShowUpgradeMenu();
        }

        if (isShowingObject)
        {
            if (isFacingRight)
            {
                objectPreview.transform.position = objectSpawnPointRight.position;
            }
            else
            {
                objectPreview.transform.position = objectSpawnPointLeft.position;
            }
        }

        //if (Input.GetButtonDown("Fire1") && canShootFireball)
        //{
        //    ShootFireball();
        //}


        if (Input.GetKeyDown(KeyCode.Mouse0) && !canShootFireball)
        {
            if (isInteracting == false)
            {
                if (catsOwned > 0)
                {
                    SpawnObject(catPrefab);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        acceleration = Mathf.Clamp(acceleration, 0f, 1f);
        if (horizontal == 0)
        {
            // decelerate
            if (IsGrounded())
            {
                acceleration = Mathf.Lerp(acceleration, 0f, 0.2f);
            }
            else
            {
                // drag
                acceleration = Mathf.Lerp(acceleration, 0f, 0.04f);
            }
            // if sprite flipped
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed * acceleration, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed * acceleration, rb.velocity.y);
            }
        }
        else
        {
            acceleration += 0.1f;
            // Move the player horizontally based on input
            rb.velocity = new Vector2(horizontal * speed * acceleration, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void SpawnObject(GameObject gameObject)
    {
        catsOwned -= 1;
        Instantiate(gameObject, objectSpawnPointRight.position, Quaternion.identity);
        if (catsOwned <= 0)
        {
            HidePreviewObject();
        }
        UpdateCatUI();
    }

    public void AddCatCurrency()
    {
        return;
    }

    public void ShowPreviewObject(Sprite sprite)
    {
        objectPreview.SetActive(true);
        objectPreview.GetComponent<SpriteRenderer>().sprite = sprite;
        isShowingObject = true;
    }

    public void HidePreviewObject()
    {
        objectPreview.SetActive(false);
        isShowingObject = false;
    }

    public void UpdateCatUI()
    {
        if (catsOwned > 0)
        {
            catUI.SetActive(true);
            catText.text = catsOwned.ToString();
        }
        else
        {
            catUI.SetActive(false);
        }
    }

    private void Flip()
    {
        if (isInteracting)
        {
            return;
        }
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
        if (isInteracting)
        {
              return;
        }
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Mouse0)&& canShootFireball)
        {
            shootTimer = shootInterval;
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        //Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        //instantiate means to spawn, quaternion means rotation
        //if (fireTutorial != null)
        //{
        //    Destroy(fireTutorial);
        //}

        if (canShootFireball && !isInteracting)
        {
            Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
            //instantiate means to spawn, quaternion means rotation
            if (fireTutorial != null)
            {
                Destroy(fireTutorial);
            }
        }
    }

    private int eyeparts;
    private int coreparts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eye"))
        {
            Destroy(collision.gameObject);
            eyeparts += 1; // Increment the parts by 1
            UpdatePartsUI(); // Update the parts UI display
        }
        if (collision.CompareTag("Core"))
        {
            Destroy(collision.gameObject);
            coreparts += 1;
            UpdatePartsUI();
        }
        if (collision.CompareTag("Merchant"))
        {
            canInteract = true;
            shopPromptText.SetActive(true);
        }

        if (collision.gameObject.CompareTag("SideCollision"))
        {
            //rb.simulated = false;

            //StartCoroutine(ReloadSceneAfterDelay(deathDelay));

            PlayerHearts playerHearts = GetComponent<PlayerHearts>();

            playerHearts.LoseAHeart();

            soundManager.PlayerHurtSound();
        }

        if (collision.CompareTag("FireballDetection"))
        {
            canShootFireball = true;

            fireballIcon.color = new Color(1f, 1f, 1f, 1f);
            fireballKey.color = new Color(1f, 1f, 1f, 1f);
        }

        //if (collision.CompareTag("FireballDetection"))
        //{
        //    UpdateFireballShot();
        //}
    }

    //private IEnumerator ReloadSceneAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Merchant"))
        {
            canInteract = false;
            shopPromptText.SetActive(false);
        }

        if (collision.CompareTag("FireballDetection"))
        {
            canShootFireball = false;
            fireballIcon.color = new Color(1f, 1f, 1f, 0.5f);
            fireballKey.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    public void ResetParts()
    {
        if (partsUI != null)
        {
            partsUI.CashOutParts(eyeparts, coreparts);
        }
        eyeparts = 0; // Set the parts value to 0
        coreparts = 0;
        UpdatePartsUI(); // Update the parts UI display
        //isInteracting = false; // Reset the interaction flag
    }

    private void ShowUpgradeMenu()
    {
        if (upgradeMenuManager != null)
        {
            upgradeMenuManager.Activate();
        }
    }

    public void UpdatePartsUI()
    {
        Parts partsUI = FindObjectOfType<Parts>();
        if (partsUI != null)
        {
            partsUI.UpdateEyesDisplay(eyeparts); 
            partsUI.UpdateCoreDisplay(coreparts);
        }
        UpgradeMenuManager upgradeMenu = FindObjectOfType<UpgradeMenuManager>();
        if (upgradeMenu != null)
        {
            upgradeMenu.UpdatePartsText(eyeparts, coreparts);
            upgradeMenu.UpdateMoneyText(partsUI.moneyValue);
        }
    }

    public void AddWizards()
    {
        wizardsCollected += 1;
        wizardCount.text = wizardsCollected.ToString();
    }
}