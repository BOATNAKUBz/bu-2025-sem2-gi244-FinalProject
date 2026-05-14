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

    // =========================
    // ⚡ Dash
    // =========================
    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing;
    private bool canDash = true;

    // =========================
    // 🔊 Sound
    // =========================
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip dashSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

        // 🎬 หา Animator
        animator =
            GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // ❌ ถ้า Dash อยู่
        if (isDashing) return;

        // =========================
        // 🎮 Movement
        // =========================
        float moveX =
            Input.GetAxisRaw(
                "Horizontal"
            );

        float moveZ =
            Input.GetAxisRaw(
                "Vertical"
            );

        moveInput =
            new Vector3(
                moveX,
                0f,
                moveZ
            ).normalized;

        // 🎬 Animation
        if (animator != null)
        {
            animator.SetBool(
                "isRunning",
                moveInput != Vector3.zero
            );
        }

        // 🖱 หันตามเมาส์
        RotateToMouse();

        // =========================
        // ⚡ Dash
        // =========================
        if (Input.GetKeyDown(
                KeyCode.LeftShift)
            &&
            canDash
            &&
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
            moveInput *
            moveSpeed *
            Time.fixedDeltaTime
        );
    }

    // =========================
    // 🖱 Rotate
    // =========================
    void RotateToMouse()
    {
        Ray ray =
            mainCamera.ScreenPointToRay(
                Input.mousePosition
            );

        if (Physics.Raycast(
                ray,
                out RaycastHit hit))
        {
            Vector3 lookPoint =
                hit.point;

            transform.LookAt(
                new Vector3(
                    lookPoint.x,
                    transform.position.y,
                    lookPoint.z
                )
            );
        }
    }

    // =========================
    // ⚡ Dash
    // =========================
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // 🔊 เสียง Dash
        if (audioSource != null
            && dashSound != null)
        {
            audioSource.PlayOneShot(
                dashSound
            );
        }

        rb.velocity =
            moveInput * dashSpeed;

        yield return new WaitForSeconds(
            dashTime
        );

        isDashing = false;

        rb.velocity =
            Vector3.zero;

        yield return new WaitForSeconds(
            dashCooldown
        );

        canDash = true;
    }
}