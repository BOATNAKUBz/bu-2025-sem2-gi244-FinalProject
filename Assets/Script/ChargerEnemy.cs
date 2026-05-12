using UnityEngine;
using System.Collections;

public class ChargerEnemy : MonoBehaviour
{
    public float detectRange = 10f;
    public float chargeSpeed = 18f;
    public float chargeDelay = 0.5f;
    public float cooldown = 2f;

    private Transform player;
    private Rigidbody rb;
    private bool canCharge = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null || !canCharge) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= detectRange)
        {
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge()
    {
        canCharge = false;

        rb.velocity = Vector3.zero;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0; // กันลอยขึ้นลง

        yield return new WaitForSeconds(chargeDelay);

        rb.velocity = dir * chargeSpeed;

        yield return new WaitForSeconds(0.4f);

        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(cooldown);

        canCharge = true;
    }
}
