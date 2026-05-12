using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}