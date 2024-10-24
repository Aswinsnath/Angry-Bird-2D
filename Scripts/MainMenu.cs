using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    public void Update()
    {
        Quit();
      //  Levell1();
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void Levell1()
    {
        SceneManager.LoadScene("Level1");
    }
}
