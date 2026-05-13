using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Ammo")]
    public int ammoAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        // 🎯 เช็ค player
        if (!other.CompareTag("Player"))
            return;

        // 🔫 หา PlayerShooting
        PlayerShooting shooting =
            other.GetComponent<PlayerShooting>();

        if (shooting != null)
        {
            // ➕ เพิ่มกระสุน
            shooting.currentAmmo += ammoAmount;

            // 🔒 กันเกิน max
            if (shooting.currentAmmo >
                shooting.maxAmmo)
            {
                shooting.currentAmmo =
                    shooting.maxAmmo;
            }

            Debug.Log(
                "🔫 Ammo +" + ammoAmount
            );
        }

        // ❌ ลบไอเทม
        Destroy(gameObject);
    }
}