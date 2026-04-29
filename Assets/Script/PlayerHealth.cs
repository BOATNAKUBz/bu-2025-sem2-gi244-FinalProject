using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        // เริ่มเกมมาให้เลือดเต็ม
        currentHealth = maxHealth;
    }

    // ฟังก์ชันสำหรับรับความเสียหาย (ใครจะโจมตีเราต้องเรียกใช้อันนี้)
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("โอ๊ย! โดนโจมตี! เลือดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("ตัวละครตายแล้ว...");
        // ใส่คำสั่งเมื่อตายตรงนี้ เช่น โหลดฉากใหม่ หรือเล่น Animation ตาย
        gameObject.SetActive(false); // หายไปจากฉาก
    }
}