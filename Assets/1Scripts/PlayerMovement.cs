using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.1f;
    public LayerMask whatIsGround;
    public float speed = 13f;
    public float jumpForce = 16f;
    private int extraJumps;
    public int extraJumpValue;
    public Button jumpButton;
    public ShopManager shopManager; // ShopManager nesnesine eri�im i�in
    public TrailRenderer trailRenderer; // Trail renderer bile�eni
    private CameraController cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpButton.onClick.AddListener(Jump);
        extraJumps = extraJumpValue;

        // Ba�lang��ta ShopManager nesnesine eri�im sa�la
        shopManager = GameObject.FindObjectOfType<ShopManager>();

        // Trail renderer bile�enini al
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false; // Ba�lang��ta devre d��� b�rak

        // Default de�erlerini g�ncelle
        UpdateDefaultValues();

        // Kamera kontrolc�s�ne eri�im sa�la
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        float moveInput = SimpleInput.GetAxis("Horizontal");

        // Z�plama kontrol�n� Update i�inde kontrol et
        if (SimpleInput.GetButtonDown("Jump") && (extraJumps > 0 || isGrounded))
        {
            Jump();
        }

        // Character'�n y�n�ne g�re animasyonu d�nd�r
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
    }

    void FixedUpdate()
    {
        float moveInput = SimpleInput.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }
    }

    void ActivateTrailRenderer()
    {
        // Trail renderer'� aktif hale getir
        trailRenderer.enabled = true;
    }

    void DisableTrailRenderer()
    {
        trailRenderer.enabled = false;
    }

    public void Jump()
    {
        Debug.Log("Jump button pressed");

        if (extraJumps > 0 || isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            cameraController.StartZoomOut();
            extraJumps--;
            SetIsJumping(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            cameraController.ResetZoom();
            isGrounded = true;
            SetIsJumping(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // E�er player ile temas ederse ve etkin de�ilse
        if (other.CompareTag("Ability"))
        {
            // Yetene�i etkinle�tir
            ActivateTrailRenderer();

            // Trail renderer'� bir sonraki frame'de aktifle�tir
            Invoke("DisableTrailRenderer", 3f);
        }

        if (other.CompareTag("Finish"))
        {
            // "Finish" objesiyle temas etti�inde "Pass" fonksiyonunu �a��r
            LevelScript levelScript = other.GetComponent<LevelScript>();
            if (levelScript != null)
            {
                levelScript.Pass();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(HandleGroundExit());
        }
    }

    private IEnumerator HandleGroundExit()
    {
        yield return new WaitForSeconds(1f);
        if (!Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround))
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
        transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    }

    // Default de�erlerini g�ncelle
    private void UpdateDefaultValues()
    {
        if (shopManager != null)
        {
            speed = shopManager.GetPlayerSpeed();
            jumpForce = shopManager.GetPlayerJumpForce();
        }
    }
}