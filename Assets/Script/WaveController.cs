using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    // 👾 ศัตรูที่ยังไม่ตาย
    private List<GameObject> aliveEnemies = new();

    // 📌 UI เอาไปใช้ได้
    public int AliveEnemyCount
    {
        get { return aliveEnemies.Count; }
    }

    // =========================================
    // 🌊 Spawn Wave
    // =========================================
    public IEnumerator SpawnWave(Wave wave)
    {
        // spawn ตามลำดับ
        for (int i = 0; i < wave.enemySequence.Length; i++)
        {
            SpawnEnemy(wave.enemySequence[i]);

            yield return new WaitForSeconds(
                wave.spawnInterval
            );
        }
    }

    // =========================================
    // 👾 Spawn Enemy
    // =========================================
    void SpawnEnemy(GameObject enemyPrefab)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No Spawn Points!");
            return;
        }

        // 🎲 สุ่มจุด spawn
        int spawnIndex = Random.Range(
            0,
            spawnPoints.Length
        );

        Transform spawnPoint = spawnPoints[spawnIndex];

        // 👾 สร้างศัตรู
        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // 📌 เก็บ enemy ที่ยังไม่ตาย
        aliveEnemies.Add(enemy);

        // 🧠 เช็คตอนตาย
        StartCoroutine(RemoveDeadEnemy(enemy));
    }

    // =========================================
    // ❌ ลบ enemy ที่ตายแล้ว
    // =========================================
    IEnumerator RemoveDeadEnemy(GameObject enemy)
    {
        // รอจน object ถูก destroy
        yield return new WaitUntil(() => enemy == null);

        aliveEnemies.Remove(enemy);
    }

    // =========================================
    // ✅ ศัตรูหมดหรือยัง
    // =========================================
    public bool IsAllEnemiesDead()
    {
        return aliveEnemies.Count == 0;
    }
}