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

    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null)
            return;

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
            rb.MovePosition(
                transform.position +
                dir *
                speed *
                Time.deltaTime
            );

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

        // ⏳ รอจังหวะตี
        yield return new WaitForSeconds(
            0.5f
        );

        if (player != null)
        {
            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            // 💥 เช็คระยะตี
            if (dist <= stopDistance + 0.5f)
            {
                PlayerHealth ph =
                    player.GetComponentInParent<PlayerHealth>();

                if (ph != null)
                {
                    ph.TakeDamage(damage);

                    Debug.Log(
                        "MELEE HIT PLAYER"
                    );
                }
            }
        }

        // 😮‍💨 cooldown
        yield return new WaitForSeconds(
            attackCooldown
        );

        canAttack = true;
    }
}