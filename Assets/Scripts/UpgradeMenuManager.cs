using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    public TextMeshProUGUI eyePartsText;
    public TextMeshProUGUI corePartsText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI valueText;

    private PlayerController playerController;

    private int value = 0;

    public bool isActive = false;

    public GameObject upgradeMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        //Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePartsText(int eyeParts, int coreParts)
    {
        eyePartsText.text = "x " + eyeParts.ToString();
        corePartsText.text = "x " + coreParts.ToString();
        value = eyeParts * 10 + coreParts * 20;
        valueText.text = "$ " + value.ToString();
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = "$ " + money.ToString();
    }

    public void CashOut()
    {
        if (playerController != null)
        {
              playerController.ResetParts();
        }
    }

    public void Activate()
    {
        isActive = true;
        playerController.isInteracting = true;
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Deactivate()
    {
        isActive = false;
        playerController.isInteracting = false;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
