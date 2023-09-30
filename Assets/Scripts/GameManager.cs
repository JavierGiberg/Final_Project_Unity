using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score;
    public Text scoreText;
    public Text highscoreText;
    public delegate void ScoreChanged(int newScore);
    public static event ScoreChanged OnScoreChanged;

    private void Awake()
    {
        highscoreText.text = "Best: " + GetHighScore().ToString();
    }

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = score.ToString(); 

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt("AdvancedToNextLevel", 0) == 1 || currentSceneIndex != 0)
        {
            StartGame();
            PlayerPrefs.SetInt("AdvancedToNextLevel", 0);
        }
    }


    public void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<Road>().StartBuilding();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !gameStarted)
        {
            StartGame();
        }
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        OnScoreChanged?.Invoke(score);
        PlayerPrefs.SetInt("Score", score);

        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
    }


    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
