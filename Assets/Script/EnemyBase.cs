using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 30;
    protected int currentHP;

    protected Transform player;

    public Action onDeath;

    protected Animator animator;

    protected virtual void Start()
    {
        currentHP = maxHP;

        GameObject playerObj =
            GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        animator =
            GetComponentInChildren<Animator>();
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        Destroy(gameObject, 2f);
    }
}