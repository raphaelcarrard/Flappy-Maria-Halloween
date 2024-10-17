using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;

    [SerializeField]
    public Text scoreText;
    public int countScore;
    int randScore;

    void Start()
    {
        randScore = Random.Range(2, 10);
        MakeInstance();
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    public void CountScore(int score){
        this.countScore += score;
        scoreText.text = "SCORE : " + this.countScore;
        GameManager.instance.IfPlayerDied(this.countScore);
    }
}
