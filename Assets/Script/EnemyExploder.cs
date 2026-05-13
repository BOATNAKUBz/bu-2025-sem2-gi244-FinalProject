using UnityEngine;
using System.Collections;

public class EnemyExploder : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float explodeRange = 2f;

    [Header("Explosion")]
    public int explosionDamage = 40;
    public float explodeDelay = 3f;

    private bool isExploding = false;

    void Update()
    {
        if (player == null || isExploding) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.forward = dir;

        float distance = Vector3.Distance(transform.position, player.position);

        // 👣 วิ่งหา player
        if (distance > explodeRange)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }
        else
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        isExploding = true;

        Debug.Log("💣 Exploder จะระเบิด!");

        yield return new WaitForSeconds(explodeDelay);

        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist <= explodeRange + 1f)
            {
                player.GetComponent<PlayerHealth>()
                    ?.TakeDamage(explosionDamage);
            }
        }

        Die();
    }
}