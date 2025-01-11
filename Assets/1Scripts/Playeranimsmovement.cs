using UnityEngine;

public class PlayerAnimsMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Karakterin y�n�ne g�re v�cut animasyonunu d�nd�r
        if (moveInput > 0)
        {
            FlipCharacter(false);
        }
        else if (moveInput < 0)
        {
            FlipCharacter(true);
        }

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            SetIsRunning(true);
        }
        else
        {
            SetIsRunning(false);
        }

        // Z�plama tu�una bas�ld���nda ve yerde ise z�pla
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        SetIsJumping(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            SetIsJumping(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            SetIsJumping(true);
        }
    }

    private void SetIsRunning(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    private void SetIsJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    private void FlipCharacter(bool isFacingLeft)
    {
        // Karakterin y�z�n� d�nd�r
        transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    }
}