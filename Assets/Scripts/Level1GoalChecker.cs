using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1GoalChecker : MonoBehaviour
{
    public FinishLevel finishLevelScript; 

    public int stairsSkipped;
    private const int goalScore=5;

    void Update()
    {
        CheckLevelGoal();
    }

    private void CheckLevelGoal()
    {
        if (goalScore <= PlayerPrefs.GetInt("Score", 0))
        {
            finishLevelScript.goalHasReached = true;
        }

    }
}