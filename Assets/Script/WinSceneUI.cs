using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{
    // ➡ ไปด่านต่อไป
    public void NextStage()
    {
        // 📌 ด่านล่าสุดที่เล่น
        int currentStage =
            PlayerPrefs.GetInt(
                "CurrentStage"
            );

        // 📌 ด่านต่อไป
        int nextStage =
            currentStage + 1;

        SceneManager.LoadScene(
            nextStage
        );
    }

    // 🏠 กลับเมนู
    public void BackMenu()
    {
        SceneManager.LoadScene(
            "MenuScene"
        );
    }
}