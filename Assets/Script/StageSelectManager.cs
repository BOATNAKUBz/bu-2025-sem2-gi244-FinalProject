using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public void LoadStage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void LoadStage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(
            "MenuScene"
        );
    }
}