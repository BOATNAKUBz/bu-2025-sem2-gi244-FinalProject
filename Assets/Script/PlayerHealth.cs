using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    private bool isDead = false;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        animator =
            GetComponentInChildren<Animator>();

        // 🔥 กันเวลาเกมค้าง
        Time.timeScale = 1f;
    }

    // =================================
    // 💥 โดนโจมตี
    // =================================
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log(
            "เลือดเหลือ: " +
            currentHealth
        );

        // ☠ ตาย
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // =================================
    // ☠ PLAYER DEAD
    // =================================
    void Die()
    {
        isDead = true;

        Debug.Log("PLAYER DEAD");

        // 🎬 Animation ตาย
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ❌ ปิดเดิน
        PlayerController pc =
            GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.enabled = false;
        }

        // ❌ ปิดยิง
        PlayerShooting ps =
            GetComponent<PlayerShooting>();

        if (ps != null)
        {
            ps.enabled = false;
        }

        // 🔥 เรียกหน้าแพ้
        StageComplete stageComplete =
            FindObjectOfType<StageComplete>();

        if (stageComplete != null)
        {
            stageComplete.LoseStage();
        }
    }
}