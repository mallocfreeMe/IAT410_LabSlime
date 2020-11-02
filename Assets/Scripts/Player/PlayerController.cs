using System;
using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Collider2D[] myColls;
        // public List<GameObject> HealthList;

        public ParticleSystem dust;

        public float speed = 7;
        public float jumpForce;
        private float moveInput;
        private float moveUp;
        private float jumpTimeCounter;
        public float jumpTime;
        public bool isJumping;

        private Rigidbody2D rb;

        private bool facingRight = true;

        public bool isGrounded;
        public Transform groundCheck;
        public float checkRadius;
        public LayerMask whatIsGround;

        public Transform enemyDetection;

        public LayerMask whatIsWall;
        private bool isTouchingWall;
        public Transform wallCheck;
        public float wallCheckDistance;

        public Animator animator;

        public float invincibleTimeAfterHurt = 2.0f;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

            moveInput = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight && moveInput < 0)
            {
                Flip();
            }
        }

        private void Update()
        {
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

        void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("PowerStone"))
            {
                Destroy(other.gameObject);
                GetComponent<Weapon>().enabled = true;
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