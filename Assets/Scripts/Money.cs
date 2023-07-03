using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int moneyAmount = 0;

    public void AddMoney(int amount)
    {
        moneyAmount += amount;
    }
}
