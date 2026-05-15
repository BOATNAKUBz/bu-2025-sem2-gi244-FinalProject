using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet")]
    public float speed = 15f;
    public float lifeTime = 3f;

    [Header("Damage")]
    public int damage = 10;

    private Rigidbody rb;

    // ====================================
    // 🧠 เริ่มต้น
    // ====================================
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 🔥 สำคัญมาก
        rb.useGravity = false;

        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;

        rb.interpolation =
            RigidbodyInterpolation.Interpolate;
    }

    // ====================================
    // 🚀 เปิดใช้งาน
    // ====================================
    void OnEnable()
    {
        CancelInvoke();

        Invoke(
            nameof(DestroyBullet),
            lifeTime
        );

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // ====================================
    // 🚀 ยิง
    // ====================================
    public void Fire(Vector3 dir)
    {
        rb.velocity =
            dir.normalized * speed;
    }

    // ====================================
    // 💥 ชน
    // ====================================
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT : " + other.name);

        // 🎯 โดน Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph =
                other.GetComponent<PlayerHealth>();

            if (ph != null)
            {
                ph.TakeDamage(damage);

                Debug.Log("PLAYER DAMAGED");
            }

            DestroyBullet();
            return;
        }

        // 🧱 ชนกำแพง
        if (other.CompareTag("Wall"))
        {
            DestroyBullet();
        }
    }

    // ====================================
    // ❌ ลบกระสุน
    // ====================================
    void DestroyBullet()
    {
        CancelInvoke();

        Destroy(gameObject);
    }
}