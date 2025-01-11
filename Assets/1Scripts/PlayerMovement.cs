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
    public ShopManager shopManager; // ShopManager nesnesine eriþim için
    public TrailRenderer trailRenderer; // Trail renderer bileþeni
    private CameraController cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpButton.onClick.AddListener(Jump);
        extraJumps = extraJumpValue;

        // Baþlangýçta ShopManager nesnesine eriþim saðla
        shopManager = GameObject.FindObjectOfType<ShopManager>();

        // Trail renderer bileþenini al
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false; // Baþlangýçta devre dýþý býrak

        // Default deðerlerini güncelle
        UpdateDefaultValues();

        // Kamera kontrolcüsüne eriþim saðla
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    void Update()
    {
        float moveInput = SimpleInput.GetAxis("Horizontal");

        // Zýplama kontrolünü Update içinde kontrol et
        if (SimpleInput.GetButtonDown("Jump") && (extraJumps > 0 || isGrounded))
        {
            Jump();
        }

        // Character'ýn yönüne göre animasyonu döndür
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
        // Trail renderer'ý aktif hale getir
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
        // Eðer player ile temas ederse ve etkin deðilse
        if (other.CompareTag("Ability"))
        {
            // Yeteneði etkinleþtir
            ActivateTrailRenderer();

            // Trail renderer'ý bir sonraki frame'de aktifleþtir
            Invoke("DisableTrailRenderer", 3f);
        }

        if (other.CompareTag("Finish"))
        {
            // "Finish" objesiyle temas ettiðinde "Pass" fonksiyonunu çaðýr
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

    // Default deðerlerini güncelle
    private void UpdateDefaultValues()
    {
        if (shopManager != null)
        {
            speed = shopManager.GetPlayerSpeed();
            jumpForce = shopManager.GetPlayerJumpForce();
        }
    }
}