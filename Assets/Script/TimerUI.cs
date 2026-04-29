using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}