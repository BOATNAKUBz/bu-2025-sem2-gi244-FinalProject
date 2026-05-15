using UnityEngine;
using System.Collections;

public class FinalBoss : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 1.5f;

    [Header("Attack Range")]
    public float attackRange = 6f;

    [Header("Attack")]
    public int damage = 25;
    public float attackCooldown = 3f;

    [Header("Warning")]
    public GameObject warningCirclePrefab;

    private bool canAttack = true;
    private bool isAttacking = false;

    // 🔥 Win System
    private StageComplete stageComplete;

    protected override void Start()
    {
        base.Start();

        // หา StageComplete ใน Scene
        stageComplete =
            FindObjectOfType<StageComplete>();
    }

    void Update()
    {
        if (player == null) return;

        // 🎯 หาทิศ player
        Vector3 dir =
            (player.position - transform.position)
            .normalized;

        dir.y = 0;

        // 👀 หันหา player
        transform.forward = dir;

        // 📏 ระยะ
        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // 👣 เดินหา player
        if (!isAttacking &&
            distance > attackRange)
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

        // ⚔ โจมตี
        if (!isAttacking &&
            canAttack &&
            distance <= attackRange)
        {
            StartCoroutine(
                AttackPattern()
            );
        }
    }

    IEnumerator AttackPattern()
    {
        canAttack = false;
        isAttacking = true;

        int attack =
            Random.Range(0, 3);

        switch (attack)
        {
            case 0:
                yield return StartCoroutine(
                    SlashAttack()
                );
                break;

            case 1:
                yield return StartCoroutine(
                    SpinAttack()
                );
                break;

            case 2:
                yield return StartCoroutine(
                    ShockwaveAttack()
                );
                break;
        }

        yield return new WaitForSeconds(
            attackCooldown
        );

        isAttacking = false;
        canAttack = true;
    }

    // =================================
    // ⚔ Slash
    // =================================
    IEnumerator SlashAttack()
    {
        Debug.Log("Slash Attack");

        if (animator != null)
        {
            animator.SetTrigger(
                "Attack"
            );
        }

        GameObject warning =
            Instantiate(
                warningCirclePrefab,
                transform.position +
                transform.forward * 3f,
                Quaternion.identity
            );

        warning.transform.localScale =
            new Vector3(
                4f,
                0.01f,
                6f
            );

        yield return new WaitForSeconds(
            1f
        );

        if (player != null)
        {
            float distance =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            if (distance < 4f)
            {
                player
                    .GetComponent<PlayerHealth>()
                    ?.TakeDamage(damage);
            }
        }

        Destroy(warning);
    }

    // =================================
    // 🌪 Spin
    // =================================
    IEnumerator SpinAttack()
    {
        Debug.Log("Spin Attack");

        if (animator != null)
        {
            animator.SetTrigger(
                "Attack"
            );
        }

        GameObject warning =
            Instantiate(
                warningCirclePrefab,
                transform.position,
                Quaternion.identity
            );

        warning.transform.localScale =
            new Vector3(
                8f,
                0.01f,
                8f
            );

        yield return new WaitForSeconds(
            1f
        );

        float spinTime = 2f;
        float timer = 0f;

        while (timer < spinTime)
        {
            transform.Rotate(
                Vector3.up *
                720 *
                Time.deltaTime
            );

            if (player != null)
            {
                float distance =
                    Vector3.Distance(
                        transform.position,
                        player.position
                    );

                if (distance < 4f)
                {
                    player
                        .GetComponent<PlayerHealth>()
                        ?.TakeDamage(1);
                }
            }

            timer += Time.deltaTime;

            yield return null;
        }

        Destroy(warning);
    }

    // =================================
    // 💥 Shockwave
    // =================================
    IEnumerator ShockwaveAttack()
    {
        Debug.Log("Shockwave");

        if (animator != null)
        {
            animator.SetTrigger(
                "Attack"
            );
        }

        GameObject warning =
            Instantiate(
                warningCirclePrefab,
                transform.position,
                Quaternion.identity
            );

        warning.transform.localScale =
            new Vector3(
                12f,
                0.01f,
                12f
            );

        yield return new WaitForSeconds(
            1.5f
        );

        if (player != null)
        {
            float distance =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            if (distance < 6f)
            {
                player
                    .GetComponent<PlayerHealth>()
                    ?.TakeDamage(
                        damage + 10
                    );
            }
        }

        Destroy(warning);
    }

    // =================================
    // ☠ ตาย
    // =================================
    protected override void Die()
    {
        Debug.Log("BOSS DEAD");

        // 🔥 ชนะด่าน
        if (stageComplete != null)
        {
            stageComplete.WinStage();

            Debug.Log("YOU WIN");
        }

        base.Die();
    }
}