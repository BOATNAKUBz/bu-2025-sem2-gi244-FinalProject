using UnityEngine;

namespace Viapix_HealingItem
{
    public class Viapix_HealingItem : MonoBehaviour
    {
        [Header("Rotate")]
        [SerializeField]
        float rotationSpeedX = 5f;

        [SerializeField]
        float rotationSpeedY = 5f;

        [SerializeField]
        float rotationSpeedZ = 5f;

        [Header("Heal")]
        [SerializeField]
        float healingAmount = 30f;

        [Header("Sound")]
        public AudioClip healSound;

        void Update()
        {
            transform.Rotate(
                rotationSpeedX,
                rotationSpeedY,
                rotationSpeedZ
            );
        }

        private void OnTriggerEnter(Collider other)
        {
            // 🎯 เช็ค Player
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth =
                    other.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    // ❤️ เพิ่มเลือด
                    playerHealth.currentHealth +=
                        healingAmount;

                    // 🔒 กันเลือดเกิน
                    if (playerHealth.currentHealth >
                        playerHealth.maxHealth)
                    {
                        playerHealth.currentHealth =
                            playerHealth.maxHealth;
                    }

                    // 🔊 เล่นเสียงฮีล
                    if (healSound != null)
                    {
                        AudioSource.PlayClipAtPoint(
                            healSound,
                            transform.position
                        );
                    }

                    // ❌ ลบไอเทม
                    Destroy(gameObject);
                }
            }
        }
    }
}