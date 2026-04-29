using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    public Transform[] spawnPoints;

    private List<GameObject> aliveEnemies = new();

    public int AliveEnemyCount
    {
        get { return aliveEnemies.Count; }
    }

    // spawn wave
    public IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave);

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    void SpawnEnemy(Wave wave)
    {
        int enemyIndex = Random.Range(0, wave.enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            wave.enemyPrefabs[enemyIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        aliveEnemies.Add(enemy);

        // ตอนศัตรูตาย → เอาออกจาก list
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            enemyBase.onDeath += () =>
            {
                aliveEnemies.Remove(enemy);
            };
        }
    }

    // เช็คว่าศัตรูตายหมดยัง
    public bool IsAllEnemiesDead()
    {
        return aliveEnemies.Count == 0;
    }
}