using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public float lifeTime = 3f; // กระสุนหายไปเองใน 3 วินาที (กันรกเครื่อง)

    void Start()
    {
        // ให้กระสุนพุ่งไปข้างหน้าตามทิศที่มันเกิด
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        // ทำลายตัวเองทิ้งเมื่อครบเวลา
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ถ้าชนสิ่งที่มีสคริปต์ Health (จากข้อความที่แล้ว)
        PlayerHealth target = other.GetComponent<PlayerHealth>();

        if (target != null)
        {
            target.TakeDamage(damage);
        }

        // ชนอะไรก็ตามให้ทำลายตัวเองทิ้งทันที
        Destroy(gameObject);
    }
}
