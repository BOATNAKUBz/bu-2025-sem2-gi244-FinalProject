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
    private Animator animator;

    // ====================================
    // 🚀 Start
    // ====================================
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // 🔥 กันล้ม
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    // ====================================
    // 🔄 Update
    // ====================================
    void Update()
    {
        if (player == null)
            return;

        // 📏 ระยะห่าง
        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // 🎯 หาทิศ
        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // ====================================
        // 👣 เดินหา Player
        // ====================================
        if (distance > stopDistance)
        {
            transform.position +=
                dir *
                speed *
                Time.deltaTime;

            // 🎬 Animation วิ่ง
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

            // 👊 โจมตี
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }

        // ====================================
        // 🎯 หันหน้าเข้าหา Player
        // ====================================
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }

    // ====================================
    // ⚔ Attack
    // ====================================
    IEnumerator Attack()
    {
        canAttack = false;

        Debug.Log("ATTACKING");

        // 🎬 Animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // ⏳ delay ก่อนโดนตี
        yield return new WaitForSeconds(0.5f);

        if (player != null)
        {
            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            // 📏 เช็คระยะอีกที
            if (dist <= stopDistance + 1f)
            {
                // 🔥 หา PlayerHealth
                PlayerHealth ph =
                    player.GetComponentInParent<PlayerHealth>();

                // ถ้าไม่มี ลองหาอีก
                if (ph == null)
                {
                    ph =
                        player.GetComponentInChildren<PlayerHealth>();
                }

                if (ph != null)
                {
                    ph.TakeDamage(damage);

                    Debug.Log("MELEE HIT PLAYER");
                }
                else
                {
                    Debug.Log("NO PLAYER HEALTH");
                }
            }
        }

        yield return new WaitForSeconds(
            attackCooldown
        );

        canAttack = true;
    }
}