using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Game : MonoBehaviour
{
    //public Button test;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
