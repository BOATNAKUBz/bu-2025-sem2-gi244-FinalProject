using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 5f;
    public int damage = 10;

    private Rigidbody rb;
    private bool canHit = false; // กันชนตอน spawn

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        CancelInvoke();

        // ตั้งเวลาให้กระสุนหาย
        Invoke(nameof(ReturnToPool), lifeTime);

        // reset ความเร็ว (สำคัญมากกับ Object Pool)
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // กันชนช่วงแรก (แก้หายแว๊บ)
        canHit = false;
        Invoke(nameof(EnableHit), 0.1f);
    }

    void EnableHit()
    {
        canHit = true;
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
        if (!canHit) return; // สำคัญมาก

        // ไม่ชน Player
        if (collision.gameObject.CompareTag("Player"))
            return;

        // โดนศัตรู
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>()?.TakeDamage(damage);
            Debug.Log("Hit: " + collision.gameObject.name);
        }

        ReturnToPool();
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