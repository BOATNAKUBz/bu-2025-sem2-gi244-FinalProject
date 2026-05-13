using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject gameOverPanel;

    private bool isDead = false;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponentInChildren<Animator>();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("โอ๊ย! โดนโจมตี! เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("ตัวละครตายแล้ว...");

        // เล่น animation ตาย
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // ปิดการขยับ
        GetComponent<PlayerController>().enabled = false;

        // รอแล้วค่อยขึ้น Game Over
        Invoke(nameof(ShowGameOver), 2f);
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}