using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public WaveSpawnManager waveManager;

    void Update()
    {
        waveText.text = "Wave " + waveManager.CurrentWave;
    }
}