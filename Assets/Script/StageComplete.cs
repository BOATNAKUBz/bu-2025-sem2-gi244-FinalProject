using UnityEngine;
using UnityEngine.SceneManagement;

public class StageComplete : MonoBehaviour
{
    // =========================
    // 🔊 หยุดเพลง
    // =========================
    void StopBGM()
    {
        BackgroundMusic bgm =
            FindObjectOfType<BackgroundMusic>();

        if (bgm != null)
        {
            Destroy(bgm.gameObject);
        }
    }

    // =========================
    // 🎉 WIN
    // =========================
    public void WinStage()
    {
        Time.timeScale = 1f;

        // 🔊 หยุดเพลง
        StopBGM();

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

        // 🔥 ถ้าเป็น Stage3
        if (currentScene == 5)
        {
            SceneManager.LoadScene(
                "AllStageClearScene"
            );
        }
        else
        {
            SceneManager.LoadScene(
                "YouWinScene"
            );
        }
    }

    // =========================
    // 💀 LOSE
    // =========================
    public void LoseStage()
    {
        Time.timeScale = 1f;

        // 🔊 หยุดเพลง
        StopBGM();

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