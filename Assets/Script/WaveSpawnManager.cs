using UnityEngine;
using System.Collections;

public class WaveSpawnManager : MonoBehaviour
{
    [Header("Wave Config")]
    public Wave[] waveConfigurations;

    [Header("Controller")]
    public WaveController waveController;

    [Header("Stage Complete")]
    public StageComplete stageComplete;

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
            Wave wave =
                waveConfigurations[currentWave];

            Debug.Log(
                "START WAVE " +
                (currentWave + 1)
            );

            // Spawn
            yield return StartCoroutine(
                waveController.SpawnWave(wave)
            );

            // รอศัตรูตายหมด
            yield return new WaitUntil(
                () => waveController.IsAllEnemiesDead()
            );

            Debug.Log("WAVE COMPLETE");

            yield return new WaitForSeconds(
                wave.waveInterval
            );

            currentWave++;
        }

        // =====================
        //  WIN
        // =====================

        Debug.Log("ALL WAVES COMPLETED");

        if (stageComplete != null)
        {
            stageComplete.WinStage();
        }
    }
}