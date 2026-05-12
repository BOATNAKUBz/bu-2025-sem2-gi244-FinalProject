using UnityEngine;
using System.Collections;

public class EnemyShooterBase : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 8f;   // ระยะหยุดเพื่อยิง
    public float retreatDistance = 3f; // ระยะถอย (กันชน)

    [Header("Attack")]
    public float fireRate = 1.2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public int damage = 10;

    private bool canShoot = true;
    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        dir.Normalize();

        // 🔴 ถ้าใกล้เกินไป → ถอย
        if (dist < retreatDistance)
        {
            rb.velocity = -dir * moveSpeed;
        }
        // 🟡 ถ้าไกลเกินระยะยิง → เดินเข้าไป
        else if (dist > stopDistance)
        {
            rb.velocity = dir * moveSpeed;
        }
        // 🟢 อยู่ในระยะยิง → หยุด
        else
        {
            rb.velocity = Vector3.zero;
        }

        // หันหน้าหาผู้เล่น
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
        }
    }

    void HandleAttack()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= stopDistance && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        Vector3 dir = (player.position - firePoint.position).normalized;

        bulletRb.velocity = dir * bulletSpeed;

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}