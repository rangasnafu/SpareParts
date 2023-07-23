using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager2 : MonoBehaviour
{
    public void LoadGame()
    {
        //SceneManager.LoadScene("SampleScene (1)");
        SceneManager.LoadScene("Story");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
