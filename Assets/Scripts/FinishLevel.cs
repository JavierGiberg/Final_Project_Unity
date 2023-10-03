using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinishLevel : MonoBehaviour
{
    public bool goalHasReached;
    private int currentScore;

    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int newScore)
    {
        currentScore = newScore;
    }

    void Update()
    {
        if (goalHasReached)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Im here");
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("AdvancedToNextLevel", 1);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("You've reached the last scene!");
        }
    }
}
