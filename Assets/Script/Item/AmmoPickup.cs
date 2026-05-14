using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Ammo")]
    public int ammoAmount = 20;

    [Header("Sound")]
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        // 🎯 เช็ค Player
        if (!other.CompareTag("Player"))
            return;

        // 🔫 หา PlayerShooting
        PlayerShooting shooting =
            other.GetComponent<PlayerShooting>();

        if (shooting != null)
        {
            // ➕ เพิ่มกระสุน
            shooting.currentAmmo += ammoAmount;

            // 🔒 กันกระสุนเกิน
            if (shooting.currentAmmo >
                shooting.maxAmmo)
            {
                shooting.currentAmmo =
                    shooting.maxAmmo;
            }

            Debug.Log(
                "🔫 Ammo +" +
                ammoAmount
            );

            // 🔊 เล่นเสียงเก็บ
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    pickupSound,
                    transform.position
                );
            }
        }

        // ❌ ลบไอเทม
        Destroy(gameObject);
    }
}