using UnityEngine;

public class BombAmmoPickup : MonoBehaviour
{
    [Header("Ammo")]
    public int ammoAmount = 5;

    [Header("Sound")]
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        // เช็ค Player
        if (!other.CompareTag("Player"))
            return;

        // หา PlayerShooting
        PlayerShooting shooting =
            other.GetComponent<PlayerShooting>();

        if (shooting != null)
        {
            // เพิ่มกระสุน
            shooting.currentBombAmmo += ammoAmount;

            // กันกระสุนเกิน
            if (shooting.currentBombAmmo >
                shooting.maxBombAmmo)
            {
                shooting.currentBombAmmo =
                    shooting.maxBombAmmo;
            }

            Debug.Log(
                "🔫 Ammo +" +
                ammoAmount
            );

            // เล่นเสียงเก็บ
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    pickupSound,
                    transform.position
                );
            }
        }

        //ลบไอเทม
        Destroy(gameObject);
    }
}