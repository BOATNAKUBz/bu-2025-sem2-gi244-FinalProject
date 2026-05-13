using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Enemy Order")]
    public GameObject[] enemySequence;

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;

    [Header("Next Wave Delay")]
    public float waveInterval = 5f;
}