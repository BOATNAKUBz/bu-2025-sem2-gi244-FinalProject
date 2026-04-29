using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ProjectileObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int initialPoolSize = 10;

    private readonly List<GameObject> projectilePool = new();

    public static ProjectileObjectPool staticinstance;
    private void Awake()
    {
        staticinstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewProjectile();
            if (i % 20 == 0)
            {
                yield return null;
            }
        }
    }
    private void CreateNewProjectile()
    {
        var go = Instantiate(projectilePrefab);
        go.SetActive(false);
        projectilePool.Add(go);
    }

    public GameObject Acquire()
    {
        if (projectilePool.Count == 0)
        {
            CreateNewProjectile();
        }
        var go = projectilePool[0];
        go.SetActive(true);
        projectilePool.RemoveAt(0);
        return go;
    }

    public void Return(GameObject projectile)
    {
        projectilePool.Add(projectile);
        projectile.SetActive(false);
    }


}