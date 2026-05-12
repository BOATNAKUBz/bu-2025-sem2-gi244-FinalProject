using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 5f;
    public int damage = 10;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // 🔥 สำคัญมาก กันทะลุ
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnEnable()
    {
        CancelInvoke();

        Invoke(nameof(ReturnToPool), lifeTime);

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // ยิงกระสุน
    public void Fire(Vector3 dir)
    {
        if (rb != null)
        {
            rb.velocity = dir.normalized * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ❌ ไม่ชนตัวเอง
        if (collision.gameObject.CompareTag("Player"))
            return;

        // 🟥 โดนศัตรู
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            ReturnToPool();
            return;
        }

        // 🧱 ชนกำแพง
        if (collision.gameObject.CompareTag("Wall"))
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        CancelInvoke();

        if (ProjectileObjectPool.staticinstance != null)
        {
            ProjectileObjectPool.staticinstance.Return(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}