using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Fire Point")]
    public Transform firePoint;

    // =========================
    //  Normal Bullet
    // =========================
    [Header("Normal Bullet")]
    public GameObject normalBulletPrefab;
    public float normalFireRate = 0.2f;

    // =========================
    // Bomb Bullet
    // =========================
    [Header("Bomb Bullet")]
    public GameObject bombBulletPrefab;
    public float bombFireRate = 1f;

    // =========================
    // Ammo
    // =========================
    [Header("Ammo")]
    public int maxAmmo = 30;
    public int currentAmmo;

    // =========================
    // Bomb Ammo
    // =========================
    [Header("Bomb Ammo")]
    public int maxBombAmmo = 5;
    public int currentBombAmmo;

    // =========================
    // 🔊 Sound
    // =========================
    [Header("Sound")]
    public AudioSource audioSource;

    public AudioClip shootSound;
    public AudioClip bombShootSound;

    private bool canShoot = true;
    private bool canBombShoot = true;

    void Start()
    {
        currentAmmo = maxAmmo;
        currentBombAmmo = maxBombAmmo;
    }

    void Update()
    {
        // ยิงปกติ
        if (Input.GetButton("Fire1")
            && canShoot
            && currentAmmo > 0)
        {
            StartCoroutine(
                NormalShootRoutine()
            );
        }

        // ยิง Bomb
        if (Input.GetButtonDown("Fire2")
            && canBombShoot
            && currentBombAmmo > 0)
        {
            StartCoroutine(
                BombShootRoutine()
            );
        }
    }

    // =========================
    // ยิงปกติ
    // =========================
    IEnumerator NormalShootRoutine()
    {
        canShoot = false;

        ShootNormal();

        yield return new WaitForSeconds(
            normalFireRate
        );

        canShoot = true;
    }

    void ShootNormal()
    {
        currentAmmo--;

        // เสียงยิงปกติ
        if (audioSource != null
            && shootSound != null)
        {
            audioSource.PlayOneShot(
                shootSound
            );
        }

        GameObject bulletObj;

        if (ProjectileObjectPool.staticinstance != null)
        {
            bulletObj =
                ProjectileObjectPool
                .staticinstance
                .Acquire();
        }
        else
        {
            bulletObj = Instantiate(
                normalBulletPrefab
            );
        }

        bulletObj.transform.position =
            firePoint.position;

        bulletObj.transform.rotation =
            firePoint.rotation;

        Bullet bullet =
            bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Fire(
                firePoint.forward
            );
        }
    }

    // =========================
    // ยิง Bomb
    // =========================
    IEnumerator BombShootRoutine()
    {
        canBombShoot = false;

        ShootBomb();

        yield return new WaitForSeconds(
            bombFireRate
        );

        canBombShoot = true;
    }

    void ShootBomb()
    {
        currentBombAmmo--;

        // เสียงยิง Bomb
        if (audioSource != null
            && bombShootSound != null)
        {
            audioSource.PlayOneShot(
                bombShootSound
            );
        }

        GameObject bombObj = Instantiate(
            bombBulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        BombBullet bomb =
            bombObj.GetComponent<BombBullet>();

        if (bomb != null)
        {
            bomb.Fire(
                firePoint.forward
            );
        }
    }

    // =========================
    // เติม Ammo
    // =========================
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    // =========================
    // 💣 เติม Bomb Ammo
    // =========================
    public void AddBombAmmo(int amount)
    {
        currentBombAmmo += amount;

        if (currentBombAmmo > maxBombAmmo)
        {
            currentBombAmmo = maxBombAmmo;
        }
    }
}