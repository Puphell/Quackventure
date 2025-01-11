using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerJump : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;

    private Animator animator; // Animator bileşeni

    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator bileşeni alınıyor
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Yatay (horizontal) hareket için "Horizontal" ve dikey (vertical) hareket için "Vertical" kullan
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;
        }

        // "Space" tuşu veya "W" tuşu
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;

            // Jump animasyonunu başlatmak için IsJumping trigger'ını tetikle
            animator.SetTrigger("IsJumping");
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;

            // Jump animasyonunu başlatmak için IsJumping trigger'ını tetikle
            animator.SetTrigger("IsJumping");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}