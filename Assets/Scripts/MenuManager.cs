using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToGallery()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
        SceneManager.LoadScene("ThanksForPlaying");
        #else
        Application.Quit();
        #endif
    }
}
