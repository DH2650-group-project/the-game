using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public BasicStats playerStats;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        // check if player is dead
        if (playerStats.currentHp <= 0)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.GetComponent<Canvas>().enabled)
        {
            // load main menu
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
