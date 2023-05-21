using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // load scene "CyberScene0"
        SceneManager.LoadScene("CyberScene0");
    }
}
