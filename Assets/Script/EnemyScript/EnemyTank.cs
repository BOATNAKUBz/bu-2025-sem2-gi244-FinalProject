using UnityEngine;
using System.Collections;

public class EnemyTank : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float stopDistance = 3f;

    [Header("Attack")]
    public int damage = 20;
    public float attackCooldown = 2f;

    private bool canAttack = true;

    void Update()
    {
        if (player == null) return;

        float dist =
            Vector3.Distance(
                transform.position,
                player.position
            );

        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // 👣 เดินหา Player
        if (dist > stopDistance)
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
            // 🛑 หยุดเดิน
            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    false
                );
            }

            // 👊 ตี
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }

        // 🎯 หันหน้า
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        // 🎬 Animation ตี
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // รอจังหวะตี
        yield return new WaitForSeconds(0.8f);

        if (player != null)
        {
            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            // 🎯 เช็คระยะตี
            if (dist <= stopDistance + 1f)
            {
                PlayerHealth ph =
                    player.GetComponentInParent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(damage);

                    Debug.Log("TANK HIT PLAYER");
                }
            }
        }

        yield return new WaitForSeconds(
            attackCooldown
        );

        canAttack = true;
    }
}