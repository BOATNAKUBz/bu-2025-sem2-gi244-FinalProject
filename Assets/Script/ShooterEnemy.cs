using UnityEngine;
using System.Collections;

public class ShooterEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 1.2f;
    public float detectRange = 15f;
    public float bulletSpeed = 20f;

    private Transform player;
    private bool canShoot = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null || !canShoot) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= detectRange)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        // หันไปหาผู้เล่น (เฉพาะแกน Y)
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;
        transform.forward = lookDir.normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 dir = (player.position - firePoint.position).normalized;

        rb.velocity = dir * bulletSpeed;

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}