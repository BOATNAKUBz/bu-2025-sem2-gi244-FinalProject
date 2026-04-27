using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 moveInput;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;    // ความเร็วตอน Dash
    public float dashTime = 0.2f;     // ระยะเวลาที่พุ่ง (วินาที)
    public float dashCooldown = 1f;  // เวลาพักก่อน Dash ได้อีกครั้ง
    private bool isDashing;           // กำลัง Dash อยู่หรือไม่
    private bool canDash = true;      // พร้อม Dash หรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ถ้ากำลัง Dash อยู่ ไม่ต้องรับ Input อื่น (ล็อกการควบคุมชั่วคราว)
        if (isDashing) return;

        // 1. การเดินปกติ
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        // 2. หันหน้าตามเมาส์
        RotateToMouse();

        // 3. กด Dash (ใช้ปุ่ม Shift ซ้าย หรือ Spacebar ก็ได้)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveInput != Vector3.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return; // ถ้า Dash อยู่ ให้ใช้แรงจาก Coroutine แทน

        // เคลื่อนที่ปกติ
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookPoint = hit.point;
            transform.LookAt(new Vector3(lookPoint.x, transform.position.y, lookPoint.z));
        }
    }

    // ฟังก์ชันพิเศษ (Coroutine) สำหรับการพุ่ง
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // ใช้ความเร็ว Dash พุ่งไปตามทิศทางที่กดเดินอยู่
        rb.velocity = moveInput * dashSpeed;

        // รอจนครบเวลา Dash
        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.velocity = Vector3.zero; // หยุดแรงพุ่ง

        // รอ Cooldown ก่อนจะ Dash ได้ใหม่
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}