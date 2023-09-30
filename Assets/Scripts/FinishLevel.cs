using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinishLevel : MonoBehaviour
{
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
        if (currentScore >= 2)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
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
