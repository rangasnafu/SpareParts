using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Parts : MonoBehaviour
{
    public TextMeshProUGUI partsText;
    public TextMeshProUGUI moneyText;

    private int moneyValue = 0;

    public void UpdatePartsDisplay(int partsValue)
    {
        partsText.text = "Parts: " + partsValue.ToString();
    }

    public void UpdateMoneyDisplay(int newMoneyValue)
    {
        moneyValue = newMoneyValue;
        moneyText.text = "$: " + moneyValue.ToString();
    }
}