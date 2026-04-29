using UnityEngine;
using System.Collections;

public class WaveSpawnManager : MonoBehaviour
{
    public Wave[] waveConfigurations;
    public WaveController waveController;

    private int currentWave = 0;
    public int CurrentWave
    {
        get { return currentWave + 1; }
    }

    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (currentWave < waveConfigurations.Length)
        {
            Wave wave = waveConfigurations[currentWave];

            Debug.Log("Start Wave: " + (currentWave + 1));

            // เริ่ม spawn
            yield return StartCoroutine(waveController.SpawnWave(wave));

            // รอจนศัตรูตายหมด
            yield return new WaitUntil(() => waveController.IsAllEnemiesDead());

            Debug.Log("Wave Complete!");

            // พักก่อน wave ถัดไป
            yield return new WaitForSeconds(wave.waveInterval);

            currentWave++;
        }

        Debug.Log("🎉 All Waves Completed!");
    }
}