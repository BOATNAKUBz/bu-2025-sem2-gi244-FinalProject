using UnityEngine;
using System.Collections;

public class EnemyExploder : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Explosion")]
    public float explodeRange = 2f;
    public int explosionDamage = 40;
    public float explodeDelay = 2f;

    [Header("Effect")]
    public GameObject explosionEffect;

    private bool isExploding = false;

    void Update()
    {
        if (player == null || isExploding)
            return;

        // 🎯 ทิศหา Player
        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // 👀 หันหน้า
        transform.forward = dir;

        // 📏 ระยะ
        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // 👣 วิ่งหา Player
        if (distance > explodeRange)
        {
            transform.position +=
                dir *
                moveSpeed *
                Time.deltaTime;

            // 🎬 วิ่ง
            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    true
                );
            }
        }
        else
        {
            // 🛑 หยุดวิ่ง
            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    false
                );
            }

            // 💣 เริ่มระเบิด
            if (!isExploding)
            {
                StartCoroutine(Explode());
            }
        }
    }

    IEnumerator Explode()
    {
        isExploding = true;

        Debug.Log("💣 EXPLODER");

        // 🎬 ใช้ animation ยิงแทนระเบิด
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        // ⏳ เวลาให้ player หนี
        yield return new WaitForSeconds(
            explodeDelay
        );

        // 💥 Effect ระเบิด
        if (explosionEffect != null)
        {
            Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
        }

        // 💥 เช็คดาเมจ
        if (player != null)
        {
            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            if (dist <= explodeRange + 1f)
            {
                PlayerHealth ph =
                    player.GetComponentInParent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(
                        explosionDamage
                    );

                    Debug.Log(
                        "PLAYER HIT BY EXPLOSION"
                    );
                }
            }
        }

        // ☠ ตาย
        Die();
    }
}