using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHearts : MonoBehaviour
{
    public int startingHearts = 3;
    public TextMeshProUGUI heartsText;
    public GameObject gameOverUI;

    private int currentHearts;
    private string heartsKey = "Hearts";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(heartsKey))
        {
            currentHearts = PlayerPrefs.GetInt(heartsKey);
        }
        else
        {
            currentHearts = startingHearts;
            PlayerPrefs.SetInt(heartsKey, currentHearts);
        }

        UpdateHeartsText();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void LoseAHeart()
    {
        currentHearts--;
        PlayerPrefs.SetInt(heartsKey, currentHearts);
        UpdateHeartsText();

        if (currentHearts <=0)
        {
            Time.timeScale = 0f;
            FindAnyObjectByType<PlayerController>().isInteracting = true;
            gameOverUI.SetActive(true);

            PlayerPrefs.DeleteAll();
        }
    }

    void UpdateHeartsText()
    {
        heartsText.text = "X " + currentHearts;
    }
}
