using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{
    // ➡ ไปด่านต่อไป
    public void NextStage()
    {
        Scene currentScene =
            SceneManager.GetActiveScene();

        // Stage1 → Stage2
        if (currentScene.name == "YouWinScene")
        {
            SceneManager.LoadScene("Stage2");
        }
    }

    // 🏠 กลับเมนู
    public void BackMenu()
    {
        SceneManager.LoadScene(
            "MenuScene"
        );
    }
}