using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // ลาก Object กระสุนมาใส่
    public Transform firePoint;     // จุดที่กระสุนจะออกจากปืน (สร้าง Empty Object ไว้ที่ปลายกระบอกปืน)
    public float fireRate = 0.2f;   // กดยิงค้างได้เร็วแค่ไหน
    private float nextFireTime = 0f;

    void Update()
    {
        // กดเมาส์ซ้ายค้างเพื่อยิง
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // สร้างลูกกระสุนออกมาจากจุด firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}