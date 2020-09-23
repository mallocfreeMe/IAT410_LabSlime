using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public EnergyBar EnergyBar;
    
    public ParticleSystem dust;
    private string spritNames = "RedTest";
    public Sprite[] Sprites;

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Sprites = Resources.LoadAll<Sprite>(spritNames);
        currentHealth = maxHealth;
        EnergyBar.SetMaxHealth(maxHealth);
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
            RaycastHit2D hasEnemyRight = Physics2D.Raycast(enemyDetection.position, Vector2.right, 4f);
            if (hasEnemyRight.collider != null && hasEnemyRight.collider.gameObject.name != "Ground")
            {
                Vector2 direction = hasEnemyRight.collider.transform.position - transform.position;
                hasEnemyRight.collider.GetComponent<Rigidbody2D>().AddForce(5.0f * direction * -1);
                //Destroy(hasEnemyRight.collider.gameObject, 2f);
                //GetComponent<SpriteRenderer>().sprite = Sprites[0];
            }
        }
        
        // health bar
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnergyBar.SetHealth(currentHealth);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Color Cube")
        {
            Destroy(other.gameObject);
            GetComponent<Weapon>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = Sprites[0];
        }
    }
}