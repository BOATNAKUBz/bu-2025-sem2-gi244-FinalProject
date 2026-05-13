using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT : " + collision.collider.name);

        PlayerHealth ph =
            collision.collider
            .GetComponentInParent<PlayerHealth>();

        if (ph != null)
        {
            ph.TakeDamage(damage);

            Debug.Log("PLAYER DAMAGED");
        }

        Destroy(gameObject);
    }
}