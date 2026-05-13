using UnityEngine;
using UnityEngine.SceneManagement;

public class StageComplete : MonoBehaviour
{
    public GameObject winPanel;

    void Start()
    {
        // 🔥 ซ่อนตอนเริ่มเกม
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    // 🎉 ชนะด่าน
    public void WinStage()
    {
        Debug.Log("SHOW WIN PANEL");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    // ➡ ด่านต่อไป
    public void NextStage()
    {
        Time.timeScale = 1f;

        int currentScene =
            SceneManager
            .GetActiveScene()
            .buildIndex;

        SceneManager.LoadScene(
            currentScene + 1
        );
    }

    // 🏠 กลับเมนู
    public void BackMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MenuScene");
    }
}