using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem dust;
    
    public float speed = 75;
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

    public Transform enemyDetection;
    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
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

    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            dust.Play();
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded)
        {
            dust.Play();
            rb.velocity = Vector2.up * jumpForce;
        }

        // absorb enemies
        if (Input.GetKey(KeyCode.S))
        {
            RaycastHit2D hasEnemyRight = Physics2D.Raycast(enemyDetection.position, Vector2.right, 2f);

            if (hasEnemyRight && enemies != null)
            {
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance <= 2f)
                    {
                        Destroy(enemy);
                    }
                }
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void CreatDust()
    {
        dust.Play();
    }
}