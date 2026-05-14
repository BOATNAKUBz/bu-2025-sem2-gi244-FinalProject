using UnityEngine;
using UnityEngine.SceneManagement;

public class StageComplete : MonoBehaviour
{
    // =========================
    // 🎉 WIN
    // =========================
    public void WinStage()
    {
        Time.timeScale = 1f;

        // 📌 เซฟด่านปัจจุบัน
        int currentScene =
            SceneManager
            .GetActiveScene()
            .buildIndex;

        PlayerPrefs.SetInt(
            "CurrentStage",
            currentScene
        );

        Debug.Log(
            "SAVE STAGE = " +
            currentScene
        );

        // ไปหน้า WIN
        SceneManager.LoadScene(
            "YouWinScene"
        );
    }

    // =========================
    // 💀 LOSE
    // =========================
    public void LoseStage()
    {
        Time.timeScale = 1f;

        // 📌 เซฟด่านปัจจุบัน
        int currentScene =
            SceneManager
            .GetActiveScene()
            .buildIndex;

        PlayerPrefs.SetInt(
            "CurrentStage",
            currentScene
        );

        Debug.Log(
            "SAVE STAGE = " +
            currentScene
        );

        // ไปหน้า LOSE
        SceneManager.LoadScene(
            "YouLoseScene"
        );
    }
}