using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private Renderer img;
    [SerializeField] private float x, y;

    void Update()
    {
        img.material.mainTextureOffset += new Vector2(x, y) * Time.deltaTime;
    }
}
