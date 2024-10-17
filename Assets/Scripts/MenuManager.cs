using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor){
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			    Destroy(o);
		    }
            SceneManager.LoadScene("ThanksForPlaying");
        }
        else if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.Android){
            Application.Quit();
        }
    }
}
