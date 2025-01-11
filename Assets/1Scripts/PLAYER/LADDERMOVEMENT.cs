using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    public float climbSpeed = 5f;

    private bool isClimbing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isClimbing)
        {
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 climbDirection = new Vector2(0, verticalInput);
            rb.velocity = climbDirection * climbSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 7; // Merdivene tırmanırken yerçekimi etkisiz olmalı
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 7; // Merdiveni terk ettiğinde yerçekimi tekrar etkin olmalı
            rb.velocity = Vector2.zero; // Merdiveni terk ettiğinde hız sıfırlanmalı
        }
    }
}