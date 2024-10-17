using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject pausePanel, gameOverPanel;

    [SerializeField]
    public Text scoreText, bestScoreText, GOBestScoreText, newHighScoreText;

    [SerializeField]
    public Button tapToStart, pauseButton;

    [SerializeField]
    public Button musicBtn;

    [SerializeField]
    public Sprite[] musicIcons;

    void Start()
    {
        Time.timeScale = 0f;
        pauseButton.gameObject.SetActive(false);
        MakeInstance();
        CheckToPlayMusic();
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    void CheckToPlayMusic(){
        if(Game.GetMusicState() == 1){
             MusicController.instance.PlayMusic(true);
             musicBtn.image.sprite = musicIcons[1];
        } else {
             MusicController.instance.PlayMusic(false);
             musicBtn.image.sprite = musicIcons[0];
        }
    }

    public void TapToStart(){
        Time.timeScale = 1f;
        tapToStart.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        bestScoreText.text = "BEST SCORE : " + Score.instance.GetHighScore();
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void RestartGame(){
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver(){
        gameOverPanel.SetActive(true);
        GOBestScoreText.text = "BEST SCORE : " + Score.instance.GetHighScore();
    }

    public void IfPlayerDied(int diedScore){
        bestScoreText.text = "BEST SCORE : " + ScoreCount.instance.countScore;
        if(diedScore > Score.instance.GetHighScore()){
            Score.instance.SetHighScore(diedScore);
            newHighScoreText.gameObject.SetActive(true);
        }
        bestScoreText.text = "BEST SCORE : " + Score.instance.GetHighScore();
    }

    public void MusicButton(){
        if(Game.GetMusicState() == 0){
            Game.SetMusicState(1);
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[1];
        }
        else if(Game.GetMusicState() == 1){
            Game.SetMusicState(0);
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[0];
        }
    }
}
