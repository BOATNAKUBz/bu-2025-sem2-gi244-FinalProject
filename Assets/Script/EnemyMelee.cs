using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase
{
    [Header("Movement")]
    public float speed = 3f;
    public float stopDistance = 2f;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private bool canAttack = true;

    void Update()
    {
        if (player == null) return;

        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // 👣 เดินหา player
        if (distance > stopDistance)
        {
            transform.position +=
                dir *
                speed *
                Time.deltaTime;

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
            if (animator != null)
            {
                animator.SetBool(
                    "isRunning",
                    false
                );
            }

            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }

        // 🎯 หันหน้า
        transform.forward = dir;
    }

    IEnumerator Attack()
    {
        canAttack = false;

        // 🎬 animation ตี
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // รอก่อนตีโดน
        yield return new WaitForSeconds(0.5f);

        if (player != null)
        {
            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            if (dist <= stopDistance + 0.5f)
            {
                PlayerHealth ph =
                    player.GetComponent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(damage);
                }
            }
        }

        yield return new WaitForSeconds(
            attackCooldown
        );

        canAttack = true;
    }
}