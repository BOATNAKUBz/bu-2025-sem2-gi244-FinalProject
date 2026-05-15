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

        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void OnEnable()
    {
        CancelInvoke();

        Invoke(nameof(ReturnToPool), lifeTime);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Fire(Vector3 dir)
    {
        rb.velocity = dir.normalized * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // ❌ ไม่ชน Player
        if (other.CompareTag("Player"))
            return;

        // 👾 โดนศัตรู
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            ReturnToPool();
        }

        // 🧱 ชนกำแพง
        if (other.CompareTag("Wall"))
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