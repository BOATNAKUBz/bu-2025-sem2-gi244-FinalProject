using UnityEngine;
using UnityEngine.SceneManagement;

public class AllStageClearUI : MonoBehaviour
{
    // 🏠 กลับเมนู
    public void BackMenu()
    {
        SceneManager.LoadScene(
            "MenuScene"
        );
    }
}