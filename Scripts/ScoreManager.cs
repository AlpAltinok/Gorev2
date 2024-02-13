using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    public Text highScoreText; 

    public static int score = 0;
    private int highScore = 0;

    private void Start()
    {
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
        score = 0;
    }

    private void Update()
    {
        
       if(CollisionManager.death==false)
            IncreaseScore(1); 
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
      /*  
        if (amount == 1000)
        {
            score += 1000;
            UpdateScoreText();
        }*/

       
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();

          
            scoreText.color = Color.green;
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = $" {score}";
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = $" {highScore}";
    }

}
