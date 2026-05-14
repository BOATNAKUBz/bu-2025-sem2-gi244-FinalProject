using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneUI : MonoBehaviour
{
    // 🔄 Retry
    public void RetryStage()
    {
        int currentStage =
            PlayerPrefs.GetInt(
                "CurrentStage"
            );

        Debug.Log(
            "LOAD STAGE = " +
            currentStage
        );

        SceneManager.LoadScene(
            currentStage
        );
    }

    // 🏠 Main Menu
    public void BackMenu()
    {
        SceneManager.LoadScene(
            "MenuScene"
        );
    }
}