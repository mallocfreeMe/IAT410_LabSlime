using System;
using Enemy;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Player
{
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
        private float moveUp;

        private Rigidbody2D rb;

        private bool facingRight = true;

        private bool isGrounded;
        public Transform groundCheck;
        public float checkRadius;
        public LayerMask whatIsGround;

        private int extraJumps;
        public int extraJumpValue;

        public Transform enemyDetection;

        public new Camera camera;
        private PostProcessVolume postProcessVolume;

        public LayerMask whatIsWall;
        private bool isTouchingWall;
        private bool isWallSliding;
        public Transform wallCheck;
        public float wallCheckDistance;
        public float wallSlideSpeed;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Sprites = Resources.LoadAll<Sprite>(spritNames);
            currentHealth = maxHealth;
            EnergyBar.SetMaxHealth(maxHealth);
            postProcessVolume = camera.GetComponent<PostProcessVolume>();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

            moveInput = Input.GetAxisRaw("Horizontal");
            moveUp = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (isWallSliding)
            {
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }

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
            if (isGrounded && isWallSliding == false)
            {
                extraJumps = extraJumpValue;
            }

            if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0 && isWallSliding == false)
            {
                dust.Play();
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded && isWallSliding == false)
            {
                dust.Play();
                rb.velocity = Vector2.up * jumpForce;
            }

            // absorb enemies
            if (Input.GetKey(KeyCode.S))
            {
                RaycastHit2D hasEnemyRight = Physics2D.Raycast(enemyDetection.position, Vector2.right, 4f);
                if (hasEnemyRight.collider != null && hasEnemyRight.collider.gameObject.GetComponent<Patrol>())
                {
                    Vector2 direction = hasEnemyRight.collider.transform.position - transform.position;
                    hasEnemyRight.collider.GetComponent<Rigidbody2D>().AddForce(5.0f * direction * -1);
                    Destroy(hasEnemyRight.collider.gameObject, 2f);
                    GetComponent<SpriteRenderer>().sprite = Sprites[0];
                }
            }

            // health bar
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(20);
            }

            CheckIfWallSliding();
        }

        private void CheckIfWallSliding()
        {
            if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            EnergyBar.SetHealth(currentHealth);
        }

        void Flip()
        {
            if (!isWallSliding)
            {
                facingRight = !facingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "Color Cube")
            {
                Destroy(other.gameObject);
                GetComponent<Weapon>().enabled = true;
                GetComponent<SpriteRenderer>().sprite = Sprites[0];
                postProcessVolume.enabled = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
}