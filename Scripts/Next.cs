using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void Nextt()
    {
        // Get the current scene's build index
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene using the build index
        // Ensure it doesn't go out of bounds
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("This is the last scene!");
            // Optionally, you can loop back to the first scene
            // SceneManager.LoadScene(0);
        }
    }
}
