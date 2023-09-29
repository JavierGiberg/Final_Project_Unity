using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool gameStarted;
    public int score;
    public Text scoreText;
    public Text highscoreText;

    private void Awake()
    {
        highscoreText.text = "Best: " + GetHighScore().ToString();
    }

    public void StartGame(){
        gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            StartGame();
        }
    }

    public void EndGame(){
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore(){
        score++;
        scoreText.text = score.ToString();

        if(score > GetHighScore()){
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }

    }

    public int GetHighScore(){
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
