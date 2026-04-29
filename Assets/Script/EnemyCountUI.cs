using UnityEngine;
using TMPro;

public class EnemyCountUI : MonoBehaviour
{
    public TextMeshProUGUI enemyText;
    public WaveController waveController;

    void Update()
    {
        enemyText.text = "x " + waveController.AliveEnemyCount;
    }
}