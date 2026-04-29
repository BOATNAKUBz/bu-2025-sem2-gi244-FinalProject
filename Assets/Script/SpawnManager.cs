using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject[] EnemyPrefabs;
    
    private int EnemyIndex;
    public float spawnRangeX = 15;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAnimal), 2f, 4f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnAnimal();
        }
    }
    void SpawnAnimal()
    {
        EnemyIndex = Random.Range(0, EnemyPrefabs.Length);
        Vector3 spawnPos = new(
            Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            transform.position.z
        );
        Instantiate(
            EnemyPrefabs[EnemyIndex],
            spawnPos,
            EnemyPrefabs[EnemyIndex].transform.rotation
        );
    }

}