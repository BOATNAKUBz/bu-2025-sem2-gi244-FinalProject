using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 30;

    protected int currentHP;

    protected Transform player;

    public Action onDeath;

    protected Animator animator;

    protected bool isDead = false;

    [Header("Death Effect")]
    public GameObject deathEffect;
    public float effectLifeTime = 2f;

    // ====================================
    // 🚀 Start
    // ====================================
    protected virtual void Start()
    {
        currentHP = maxHP;

        GameObject playerObj =
            GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        animator =
            GetComponentInChildren<Animator>();
    }

    // ====================================
    // 💥 รับดาเมจ
    // ====================================
    public virtual void TakeDamage(int dmg)
    {
        // ❌ ถ้าตายแล้วไม่รับดาเมจ
        if (isDead)
            return;

        currentHP -= dmg;

        Debug.Log(
            gameObject.name +
            " HP : " +
            currentHP
        );

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // ====================================
    // ☠ ตาย
    // ====================================
    protected virtual void Die()
    {
        // ❌ กันตายซ้ำ
        if (isDead)
            return;

        isDead = true;

        // 🛑 ปิด script อื่น
        MonoBehaviour[] scripts =
            GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }

       
        // 🛑 หยุด Rigidbody
        Rigidbody rb =
            GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 🎬 Animation ตาย
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ✨ Effect ระเบิด
        if (deathEffect != null)
        {
            GameObject effect =
           Instantiate(
                deathEffect,
                transform.position,
                Quaternion.identity
            );
            Destroy(
                  effect,
                  effectLifeTime
              );
        }

        // 📢 แจ้งระบบ wave
        onDeath?.Invoke();

        // ❌ ลบ enemy
        Destroy(gameObject, 1f);

    }
}