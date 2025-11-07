using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;

    private AudioSource audioSource;
    public AudioClip menuAndGameMusic;
    public AudioClip galleryMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                ChangeMusic(menuAndGameMusic);
                break;
            case "Gameplay":
                ChangeMusic(menuAndGameMusic);
                break;
            case "Gallery":
                ChangeMusic(galleryMusic);
                break;
            case "ThanksForPlaying":
                Destroy(audioSource);
                break;
        }
    }

    void ChangeMusic(AudioClip newClip)
    {
        if (audioSource == null)
        {
            return;
        }
        if (audioSource.clip == newClip)
        {
            return;
        }
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }

    public void PlayMusic(bool play){
        if(play){
            if(!audioSource.isPlaying){
                audioSource.Play();
            }
        } else {
            if(audioSource.isPlaying){
                audioSource.Stop();
            }
        }
    }
}
