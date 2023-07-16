using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Parts : MonoBehaviour
{
    public TextMeshProUGUI eyepartsText;
    public TextMeshProUGUI corepartsText;
    public TextMeshProUGUI moneyText;

    public int moneyValue = 0;

    public void UpdateEyesDisplay(int eyepartsValue)
    {
        eyepartsText.text = "X " + eyepartsValue.ToString();
    }

    public void UpdateCoreDisplay(int corepartsValue)
    {
        corepartsText.text = "X " + corepartsValue.ToString();
    }

    public void UpdateMoneyDisplay(int newMoneyValue)
    {
        moneyValue = newMoneyValue;
        moneyText.text = "$: " + moneyValue.ToString();
    }

    public void CashOutParts(int eyepartsValue, int corepartsValue)
    {
        moneyValue += eyepartsValue * 10;
        moneyValue += corepartsValue * 20;
        UpdateMoneyDisplay(moneyValue);
    }
}