using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score;
    public Text scoreText;
    public Text highscoreText;
    public delegate void ScoreChanged(int newScore);
    public static event ScoreChanged OnScoreChanged;
    private InputAction startGameAction;

    private void OnEnable()
    {
        startGameAction.Enable(); 
    }

    private void OnDisable()
    {
        startGameAction.Disable(); 
    }

    private void Awake()
    {
        highscoreText.text = "Best: " + GetHighScore().ToString();
        startGameAction = new InputAction(binding: "<Keyboard>/downArrow");
        startGameAction.performed += ctx => StartGame();
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



    private void Update()
    {


    }


    public void StartGame()
    {
        gameStarted = true;
        Debug.Log("Game Started: " + gameStarted);
        FindObjectOfType<Road>().StartBuilding();
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
