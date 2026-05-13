using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHealth hp =
            other.GetComponent<PlayerHealth>();

        if (hp != null)
        {
            hp.currentHealth += healAmount;

            if (hp.currentHealth > hp.maxHealth)
            {
                hp.currentHealth = hp.maxHealth;
            }

            Debug.Log("❤️ Heal");
        }

        Destroy(gameObject);
    }
}