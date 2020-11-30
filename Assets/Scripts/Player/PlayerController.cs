using System;
using System.Collections;
using Enemy;
using UnityEngine;
using Cinemachine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        public ParticleSystem dust;
        public Transform groundCheck;
        public LayerMask whatIsGround;
        //public CinemachineCameraOffset lookdown;
        public CinemachineCameraOffset m_ApplyAfter;
        GameObject camObj;
        CinemachineFreeLook look;
        CinemachineComposer comp;


        public float speed = 7;
        public float jumpForce;
        private float moveInput;
        private float moveUp;
        private float jumpTimeCounter;
        public float jumpTime;
        public bool isJumping;
        public bool facingRight = true;
        public bool isGrounded;
        public float checkRadius;
        public float invincibleTimeAfterHurt = 2.0f;

        private PlayerDash _playerDashScript;
        private PlayerFloat _playerFloatScript;

        public AudioSource audioSource;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            _playerDashScript = GetComponent<PlayerDash>();
            _playerFloatScript = GetComponent<PlayerFloat>();

            camObj = GameObject.FindWithTag("MainCamera");
            look = camObj.GetComponent<CinemachineFreeLook>();
            comp = look.GetRig(1).GetCinemachineComponent<CinemachineComposer>();

        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxisRaw("Horizontal");
            
            // if player is not dashing
            if (_playerDashScript.direction == 0)
            {
                rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            }
            
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
            if (isGrounded && ( Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                dust.Play();
                audioSource.Play();
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }

            if ( (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isJumping)
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

            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
            {
                isJumping = false;
            }

            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {

                comp.m_TrackedObjectOffset.y = 5;

            }
        }
    }
}