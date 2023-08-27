using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text highScores1Text;
    public Text highScores2Text;
    public Text scoreText;
    public static int score=0;
    public static int highScore1;
    public static int highScore2;

    private void Start()
    {
        
        //if (PlayerPrefs.HasKey("HighScore"))
        //{
        //    highScore = PlayerPrefs.GetInt("HiScore");
        //}
        score = PlayerPrefs.GetInt("Score");
        highScore1 = PlayerPrefs.GetInt("HighScore1P");
        highScore2 = PlayerPrefs.GetInt("HighScore2P");
    }
    void Update()
    {
        //if (score > highScore) 
        //{
        //    highScore = score;
        //    PlayerPrefs.SetInt("HiScore", highScore);
        //}
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = "Score: " + Mathf.Round(score);
        highScores1Text.text = "1 Player Highscore: " + Mathf.Round(highScore1);
        highScores2Text.text = "2 Players Highscore:" + Mathf.Round(highScore2);
    }

    public void AddScore(int points)
    {
        score += points;
    }
}
