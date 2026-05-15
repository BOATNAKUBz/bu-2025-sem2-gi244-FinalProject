using UnityEngine;

public class ProjectileReturnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Player Bullet
        Bullet bullet =
            other.GetComponent<Bullet>();

        if (bullet != null)
        {
            // คืนเข้า Pool
            if (ProjectileObjectPool.staticinstance != null)
            {
                ProjectileObjectPool
                    .staticinstance
                    .Return(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }

            return;
        }

        // Bomb Bullet
        BombBullet bomb =
            other.GetComponent<BombBullet>();

        if (bomb != null)
        {
            Destroy(other.gameObject);
            return;
        }

        // Enemy Bullet
        EnemyBullet enemyBullet =
            other.GetComponent<EnemyBullet>();

        if (enemyBullet != null)
        {
            // ทำลายเลย
            Destroy(other.gameObject);
            return;
        }
    }
}