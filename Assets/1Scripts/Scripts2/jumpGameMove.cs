using UnityEngine;
using UnityEngine.EventSystems;

public class JumpGameMove : MonoBehaviour
{
    public float speed = 13f;
    public float jumpForce = 16f;
    private Rigidbody2D rb;
    private float moveInput = 0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Character'ýn yönüne göre animasyonu döndür
        if (moveInput > 0)
        {
            FlipCharacter(false);
        }
        else if (moveInput < 0)
        {
            FlipCharacter(true);
        }
    }

    public void Jump()
    {
        // Karakteri zýplat
        rb.velocity = Vector2.up * jumpForce;
    }

    public void MoveLeft()
    {
        moveInput = -1f;
    }

    public void MoveRight()
    {
        moveInput = 1f;
    }

    public void StopMoving()
    {
        moveInput = 0f;
    }

    private void FlipCharacter(bool isFacingLeft)
    {
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(isFacingLeft ? -Mathf.Abs(currentScale.x) : Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
    }
}
