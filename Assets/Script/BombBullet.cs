using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float lifeTime = 5f;

    [Header("Explosion")]
    public float explosionRadius = 5f;
    public int damage = 40;

    [Header("Effect")]
    public GameObject explosionEffect;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // ยิง
    public void Fire(Vector3 dir)
    {
        rb.velocity = dir.normalized * speed;
    }

    // ชนอะไรก็ระเบิด
    void OnTriggerEnter(Collider other)
    {
        // ไม่ชน player
        if (other.CompareTag("Player"))
            return;

        Explode();
    }

    // ระเบิด
    void Explode()
    {
        // Effect
        if (explosionEffect != null)
        {
            Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
        }

        //  หา object รอบระเบิด
        Collider[] hitColliders =
            Physics.OverlapSphere(
                transform.position,
                explosionRadius
            );

        foreach (Collider hit in hitColliders)
        {
            // โดน enemy
            if (hit.CompareTag("Enemy"))
            {
                EnemyBase enemy =
                    hit.GetComponent<EnemyBase>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    // ดูระยะใน Scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}