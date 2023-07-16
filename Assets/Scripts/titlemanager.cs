using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene (1)");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial"); 
    }
}
