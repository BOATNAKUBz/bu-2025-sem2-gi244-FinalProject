using UnityEngine;
using System.Collections;

public class ItemSpawnManager : MonoBehaviour
{
    [Header("Item Prefabs")]
    public GameObject[] itemPrefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f;

    public bool randomSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnItem();

            yield return new WaitForSeconds(
                spawnInterval
            );
        }
    }

    void SpawnItem()
    {
        //  ไม่มี item
        if (itemPrefabs.Length == 0)
            return;

        //  ไม่มีจุด spawn
        if (spawnPoints.Length == 0)
            return;

        // สุ่ม item
        int itemIndex =
            Random.Range(
                0,
                itemPrefabs.Length
            );

        // สุ่มจุด spawn
        int pointIndex =
            Random.Range(
                0,
                spawnPoints.Length
            );

        GameObject item =
            itemPrefabs[itemIndex];

        Transform point =
            spawnPoints[pointIndex];

        // ✨ Spawn
        Instantiate(
            item,
            point.position,
            point.rotation
        );
    }
}