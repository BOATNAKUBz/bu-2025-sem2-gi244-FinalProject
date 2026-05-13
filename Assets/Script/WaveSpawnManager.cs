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

    // 📌 เอาไว้โชว์ UI
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
                "🌊 Start Wave: " +
                (currentWave + 1)
            );

            // 👾 Spawn wave
            yield return StartCoroutine(
                waveController.SpawnWave(wave)
            );

            // ⏳ รอศัตรูตายหมด
            yield return new WaitUntil(
                () => waveController.IsAllEnemiesDead()
            );

            Debug.Log("✅ Wave Complete!");

            // ⏳ พักก่อน wave ใหม่
            yield return new WaitForSeconds(
                wave.waveInterval
            );

            currentWave++;
        }

        // =================================
        // 🎉 ผ่านทุก Wave
        // =================================
        Debug.Log("🎉 ALL WAVES COMPLETED!");

        // 🔥 YOU WIN
        if (stageComplete != null)
        {
            Debug.Log("WIN STAGE");

            stageComplete.WinStage();
        }
        else
        {
            Debug.LogError(
                "StageComplete is NULL!"
            );
        }
    }
}