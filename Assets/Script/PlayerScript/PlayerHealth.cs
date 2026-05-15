using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Sound")]
    public AudioSource audioSource;

    public AudioClip hurtSound;
    public AudioClip deathSound;

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

        // 🔊 เสียงโดนตี
        if (audioSource != null &&
            hurtSound != null)
        {
            audioSource.PlayOneShot(
                hurtSound
            );
        }

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

        // 🔊 เล่นเสียงตาย
        if (audioSource != null &&
            deathSound != null)
        {
            audioSource.PlayOneShot(
                deathSound
            );
        }

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

        // ⏳ รอก่อนเข้าแพ้
        Invoke(nameof(DelayLose), 2f);
    }

    // =================================
    // 💀 เข้า You Lose
    // =================================
    void DelayLose()
    {
        StageComplete stageComplete =
            FindObjectOfType<StageComplete>();

        if (stageComplete != null)
        {
            stageComplete.LoseStage();
        }
    }
}