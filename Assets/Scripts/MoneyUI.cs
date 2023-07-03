using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartsUI : MonoBehaviour
{
    public TextMeshProUGUI partsText;

    public void UpdatePartsDisplay(int partsValue)
    {
        partsText.text = "Parts: " + partsValue.ToString();
    }
}