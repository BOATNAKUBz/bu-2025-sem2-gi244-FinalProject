using UnityEngine;
using System.Collections;

[System.Serializable]
public class BulletData
{
    public string bulletName;
    public GameObject bulletPrefab; // ใช้ fallback ถ้าไม่มี pool
    public float fireRate = 0.2f;
}

public class PlayerShooting : MonoBehaviour
{
    [Header("Fire Point")]
    public Transform firePoint;

    [Header("Bullet Types")]
    public BulletData[] bullets;

    private int currentBulletIndex = 0;
    private bool canShoot = true;

    [Header("Ammo")]
    public int maxAmmo = 30;
    public int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // 🔫 ยิง
        if (Input.GetButton("Fire1") && canShoot && currentAmmo > 0)
        {
            StartCoroutine(ShootRoutine());
        }

        // 🔄 เปลี่ยนกระสุน
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchBullet();
        }
    }

    IEnumerator ShootRoutine()
    {
        canShoot = false;

        Shoot();

        yield return new WaitForSeconds(bullets[currentBulletIndex].fireRate);
        canShoot = true;
    }

    void Shoot()
{
        currentAmmo--;

        if (firePoint == null || bullets.Length == 0) return;

    GameObject bulletObj;

    if (ProjectileObjectPool.staticinstance != null)
    {
        bulletObj = ProjectileObjectPool.staticinstance.Acquire();
    }
    else
    {
        bulletObj = Instantiate(
            bullets[currentBulletIndex].bulletPrefab
        );
    }

    bulletObj.transform.position = firePoint.position;
    bulletObj.transform.rotation = firePoint.rotation;

    Bullet bullet = bulletObj.GetComponent<Bullet>();
    if (bullet != null)
    {
        bullet.Fire(firePoint.forward);
    }
}
    void SwitchBullet()
    {
        if (bullets.Length == 0) return;

        currentBulletIndex = (currentBulletIndex + 1) % bullets.Length;

        Debug.Log("Current Bullet: " + bullets[currentBulletIndex].bulletName);
    }
}