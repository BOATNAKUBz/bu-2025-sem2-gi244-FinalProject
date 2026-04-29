using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10f;

    // เมื่อตัวละครเดินมาชน (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าสิ่งที่มาชนมีสคริปต์ PlayerHealth หรือเปล่า
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}