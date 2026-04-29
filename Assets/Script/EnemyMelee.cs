using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBase
{
    [Header("Movement")]
    public float speed = 3f;
    public float stopDistance = 1.5f;

    [Header("Attack")]
    public int damage = 10;
    public float attackCooldown = 1f;

    private bool canAttack = true;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // หาทิศไปหา Player
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        // เดินเข้าไป
        if (distance > stopDistance)
        {
            transform.position += dir * speed * Time.deltaTime;
        }
        else
        {
            // อยู่ใกล้ → ตี
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }

        // หันหน้าหา Player
        transform.forward = dir;
    }

    IEnumerator Attack()
    {
        canAttack = false;

        // เช็คระยะก่อนตี
        if (player != null &&
            Vector3.Distance(transform.position, player.position) <= stopDistance)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Enemy hit player!");
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
