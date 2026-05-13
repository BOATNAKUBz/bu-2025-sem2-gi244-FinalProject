using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 moveInput;
    private Animator animator;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        // หา Animator จากโมเดลลูก
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // ถ้า Dash อยู่ ไม่รับ input
        if (isDashing) return;

        // รับ input เดิน
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        // ส่งค่าไป Animator
        if (animator != null)
        {
            animator.SetBool("isRunning", moveInput != Vector3.zero);
        }

        // หันตามเมาส์
        RotateToMouse();

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            canDash &&
            moveInput != Vector3.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        rb.MovePosition(
            rb.position +
            moveInput * moveSpeed * Time.fixedDeltaTime
        );
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookPoint = hit.point;

            transform.LookAt(
                new Vector3(
                    lookPoint.x,
                    transform.position.y,
                    lookPoint.z
                )
            );
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.velocity = moveInput * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}