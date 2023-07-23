using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public Button catButton;
    public Button wizardButton;

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
        RefreshButtons();
    }

    public void CashOut()
    {
        if (playerController != null)
        {
              playerController.ResetParts();
        }
        RefreshButtons();
    }

    public void Activate()
    {
        isActive = true;
        playerController.isInteracting = true;
        upgradeMenu.SetActive(true);
        Time.timeScale = 0f;
        RefreshButtons();
    }

    public void Deactivate()
    {
        isActive = false;
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
        RefreshButtons();
        playerController.isInteracting = true;
        Invoke("DelayedPlayerControl", 0.1f);
    }

    private void DelayedPlayerControl()
    {
        playerController.isInteracting = false;
    }

    public void RefreshButtons()
    {
        if (playerController != null)
        {
            if (playerController.partsUI.moneyValue >= 5)
            {
                catButton.interactable = true;
            }
            else
            {
                catButton.interactable = false;
            }
            if (playerController.partsUI.moneyValue >= 10)
            {
                wizardButton.interactable = true;
            }
            else
            {
                wizardButton.interactable = false;
            }
        }
    }

    public void PurchaseCatItem()
    {
        if (playerController != null)
        {
            if (playerController.partsUI.moneyValue >= 5)
            {
                playerController.partsUI.moneyValue -= 5;
                playerController.partsUI.UpdateMoneyDisplay(playerController.partsUI.moneyValue);
                playerController.catsOwned += 1;
                playerController.catText.text = playerController.catsOwned.ToString();
                playerController.catUI.SetActive(true);
                playerController.objectPreview.SetActive(true);
            }
        }
        RefreshButtons();
        UpdateMoneyText(playerController.partsUI.moneyValue);
        playerController.UpdateCatUI();
        playerController.UpdatePartsUI();
    }

    public void PurchaseWizardItem()
    {
        if (playerController != null)
        {
            if (playerController.partsUI.moneyValue >= 40)
            {
                playerController.partsUI.moneyValue -= 40;
                playerController.partsUI.UpdateMoneyDisplay(playerController.partsUI.moneyValue);
                playerController.AddWizards();
            }
        }
        RefreshButtons();
        UpdateMoneyText(playerController.partsUI.moneyValue);
        playerController.UpdateCatUI();
        playerController.UpdatePartsUI();
    }
}
