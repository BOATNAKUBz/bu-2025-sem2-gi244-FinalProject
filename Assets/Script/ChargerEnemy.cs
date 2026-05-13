using UnityEngine;
using System.Collections;

public class EnemyCharger : EnemyBase
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 3f;

    [Header("Attack")]
    public int damage = 20;

    private bool isDashing = false;
    private bool canDash = true;

    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        transform.forward = dir;

        // 👣 เดินปกติ
        if (!isDashing)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

        // ⚡ Dash
        if (canDash)
        {
            StartCoroutine(Dash(dir));
        }
    }

    IEnumerator Dash(Vector3 dir)
    {
        canDash = false;
        isDashing = true;

        rb.velocity = dir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
                .GetComponent<PlayerHealth>()
                ?.TakeDamage(damage);
        }
    }
}