using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 30;
    protected int currentHP;

    protected Transform player; // อ้างอิง Player

    public Action onDeath; // แจ้งตอนตาย (ใช้กับ Wave)

    protected virtual void Start()
    {
        currentHP = maxHP;

        // หา Player จาก Tag
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! กรุณาตั้ง Tag = Player");
        }
    }

    // รับดาเมจ
    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        Debug.Log(gameObject.name + " HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // ตาย
    protected virtual void Die()
    {
        onDeath?.Invoke(); // แจ้ง WaveController
        Destroy(gameObject);
    }
}