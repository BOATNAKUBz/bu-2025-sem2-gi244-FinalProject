using UnityEngine;
using System.Collections;

public class EnemyShooterBase : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 8f;
    public float retreatDistance = 3f;

    [Header("Attack")]
    public float fireRate = 1.2f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int damage = 10;

    private bool canShoot = true;

    void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        float dist =
            Vector3.Distance(
                transform.position,
                player.position
            );

        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // 🔴 ถอย
        if (dist < retreatDistance)
        {
            transform.position +=
                -dir *
                moveSpeed *
                Time.deltaTime;

            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    true
                );
            }
        }

        // 🟡 เดินเข้า
        else if (dist > stopDistance)
        {
            transform.position +=
                dir *
                moveSpeed *
                Time.deltaTime;

            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    true
                );
            }
        }

        // 🟢 หยุดยิง
        else
        {
            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    false
                );
            }
        }

        // 🎯 หันหน้า
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }

    void HandleAttack()
    {
        float dist =
            Vector3.Distance(
                transform.position,
                player.position
            );

        if (dist <= stopDistance && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        // 🎬 Animation ยิง
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        // 🎯 จุดยิง
        Vector3 shootPos =
            transform.position +
            transform.forward * 1.2f +
            Vector3.up * 1.2f;

        // 🎯 ทิศยิง
        Vector3 dir =
            (player.position + Vector3.up)
            - shootPos;

        dir.Normalize();

        // 🔥 สร้างกระสุน
        GameObject bullet =
            Instantiate(
                bulletPrefab,
                shootPos,
                Quaternion.LookRotation(dir)
            );

        Rigidbody rb =
            bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity =
                dir * bulletSpeed;
        }

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}