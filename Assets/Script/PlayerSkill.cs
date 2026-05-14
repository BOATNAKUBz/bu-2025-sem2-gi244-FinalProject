using UnityEngine;
using System.Collections;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill")]
    public float radius = 5f;
    public int damage = 30;
    public float cooldown = 5f;

    [Header("Effect")]
    public GameObject effectPrefab;

    // ⏳ เวลาลบ effect
    public float effectLifeTime = 2f;

    private bool canUse = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
            && canUse)
        {
            StartCoroutine(UseSkill());
        }
    }

    IEnumerator UseSkill()
    {
        canUse = false;

        Debug.Log("AOE SKILL");

        // ✨ สร้าง effect
        if (effectPrefab != null)
        {
            GameObject effect =
                Instantiate(
                    effectPrefab,
                    transform.position,
                    Quaternion.identity
                );

            // 🔥 ลบ effect อัตโนมัติ
            Destroy(
                effect,
                effectLifeTime
            );
        }

        // 🔍 หา enemy รอบตัว
        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                radius
            );

        foreach (Collider hit in hits)
        {
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

        // ⏳ cooldown
        yield return new WaitForSeconds(
            cooldown
        );

        canUse = true;
    }

    // 🔴 วาดวงใน Scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(
            transform.position,
            radius
        );
    }
}