using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}