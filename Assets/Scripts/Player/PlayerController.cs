using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Enemy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Collider2D[] myColls;
        public int maxHealth = 100;
        public int currentHealth;
        public EnergyBar EnergyBar;
        // public List<GameObject> HealthList;

        public ParticleSystem dust;
        private string spritNames = "RedTest";
        public Sprite[] Sprites;

        public float speed = 7;
        public float jumpForce;
        private float moveInput;
        private float moveUp;
        private float jumpTimeCounter;
        public float jumpTime;
        private bool isJumping;

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

        public Animator animator;

        public float invincibleTimeAfterHurt = 2.0f;

        // private bool isConsuming;

        public Tilemap ground;
        public Tilemap box;

        public Light2D globalLight;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Sprites = Resources.LoadAll<Sprite>(spritNames);
            currentHealth = maxHealth;
            // EnergyBar.SetMaxHealth(maxHealth);
            postProcessVolume = camera.GetComponent<PostProcessVolume>();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

            moveInput = Input.GetAxisRaw("Horizontal");
            // moveUp = Input.GetAxis("Vertical");

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
            //rb.AddForce(Vector2.right * 100);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            if (isGrounded && Input.GetKeyDown(KeyCode.W))
            {
                dust.Play();
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }

            if (Input.GetKey(KeyCode.W) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
            }

            /*if (isGrounded && isWallSliding == false)
            {
                extraJumps = extraJumpValue;
            }

            if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0 && isWallSliding == false)
            {
                animator.SetTrigger("Jump");
                dust.Play();
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded && isWallSliding == false)
            {
                animator.SetTrigger("Jump");
                dust.Play();
                rb.velocity = Vector2.up * jumpForce;
            }*/

            // absorb enemies
            /*if (Input.GetKey(KeyCode.S))
            {
                RaycastHit2D hasEnemyRight = Physics2D.Raycast(enemyDetection.position, Vector2.right, 640f);
                if (hasEnemyRight.collider != null && hasEnemyRight.collider.gameObject.GetComponent<Patrol>())
                {
                    hasEnemyRight.collider.gameObject.GetComponent<Patrol>().speed = 0;
                    isConsuming = true;
                    Vector2 direction = hasEnemyRight.collider.transform.position - transform.position;
                    rb.AddForce(5.0f * direction * 1);
                    Destroy(hasEnemyRight.collider.gameObject, 2f);
                    GetComponent<SpriteRenderer>().color = Color.red;
                    animator.SetBool("IsEating", true);
                    //animator.SetBool("IsRed", true);
                }
                else
                {
                    isConsuming = false;
                    animator.SetBool("IsEating", false);
                }
            }

            if (isConsuming)
            {
                rb.AddForce(Vector2.right * 100);
            }*/

            CheckIfWallSliding();

            /*if (HealthList.Count == 0)
            {
                SceneManager.LoadScene(2);
                int enemyLayer = LayerMask.NameToLayer("Enemy");
                int magmaLayer = LayerMask.NameToLayer("Magma");
                int trapLayer = LayerMask.NameToLayer("Trap");
                int playerLayer = LayerMask.NameToLayer("Player");
                Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(magmaLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(trapLayer, playerLayer, false);
            }*/
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
            if (other.gameObject.CompareTag("PowerStone"))
            {
                Destroy(other.gameObject);
                GetComponent<Weapon>().enabled = true;
                extraJumpValue = 9;
                globalLight.color = new Color(227f/255f, 83f/255f, 83f/255f);
            }

            if (other.gameObject.GetComponent<Patrol>())
            {
                StartCoroutine(HurtBlinker());
            }

            if ((other.gameObject.name == "Magma" || other.gameObject.name == "Trap"))
            {
                StartCoroutine(HurtBlinkerForEnvironment(other.gameObject.name));
            }
        }

        IEnumerator HurtBlinker()
        {
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, true);
            foreach (Collider2D collider2D in myColls)
            {
                collider2D.enabled = false;
                collider2D.enabled = true;
            }
            animator.SetLayerWeight(1, 1);
            /*if (HealthList.Count > 0)
            {
                HealthList[HealthList.Count - 1].SetActive(false);
                HealthList.Remove(HealthList[HealthList.Count - 1]);
            }*/
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            animator.SetLayerWeight(1, 0);
        }
        
        IEnumerator HurtBlinkerForEnvironment(string layerName)
        {
            int enemyLayer = LayerMask.NameToLayer(layerName);
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
            foreach (Collider2D collider2D in myColls)
            {
                collider2D.enabled = false;
                collider2D.enabled = true;
            }
            animator.SetLayerWeight(1, 1);
            /* if (HealthList.Count > 0)
            {
                HealthList[HealthList.Count - 1].SetActive(false);
                HealthList.Remove(HealthList[HealthList.Count - 1]);
            } */
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            animator.SetLayerWeight(1, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(wallCheck.position,
                new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
}