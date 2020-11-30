using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private PlayerController playerControllerScript;
        private PlayerSkill playerSkillScript;
        private Animator animator;

        private PlayerFloat _playerFloatScript;

        void Start()
        {
            playerControllerScript = GetComponent<PlayerController>();
            playerSkillScript = GetComponent<PlayerSkill>();
            animator = GetComponent<Animator>();
            _playerFloatScript = GetComponent<PlayerFloat>();
        }

        void Update()
        {
            // move animations 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedIsRunning", true);
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueIsRunning", true);
                }
                else if (animator.GetBool("IsGreen"))
                {
                    animator.SetBool("GreenIsRunning", true);
                }
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue") && !animator.GetBool("IsGreen"))
                {
                    animator.SetBool("IsRunning", true);
                }
            }
            else
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedIsRunning", false);
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueIsRunning", false);
                }
                else if (animator.GetBool("IsGreen"))
                {
                    animator.SetBool("GreenIsRunning", false);
                }
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue") && !animator.GetBool("IsGreen"))
                {
                    animator.SetBool("IsRunning", false);
                }
            }

            // jump animations 
            if (playerControllerScript.isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetTrigger("RedJump");
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetTrigger("BlueJump");
                }
                else if (animator.GetBool("IsGreen"))
                {
                    animator.SetTrigger("GreenJump");
                }
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue") && !animator.GetBool("IsGreen"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            if (!playerControllerScript.isJumping && playerControllerScript.isGrounded)
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedOnGround", true);
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueOnGround", true);
                }
                else if (animator.GetBool("IsGreen"))
                {
                    animator.SetBool("GreenOnGround", true);
                }
                else
                {
                    //animator.SetTrigger("Jump");
                }
            }
            else
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedOnGround", false);
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueOnGround", false);
                }
                else if (animator.GetBool("IsGreen"))
                {
                    animator.SetBool("GreenOnGround", false);
                }
                else
                {
                    //animator.SetTrigger("Jump");
                }
            }

            // red dash
            if (animator.GetBool("IsRed"))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    animator.SetTrigger("RedDash");
                }
            }
            
            // blue float
            if (animator.GetBool("IsBlue"))
            {
                if (_playerFloatScript.isFloating)
                {
                    animator.SetBool("BlueFloat", true);
                }
                else
                {
                    animator.SetBool("BlueFloat", false);
                }
            }
            
            // green shoot projectiles
            if (animator.GetBool("IsGreen"))
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    animator.SetTrigger("GreenShoot");
                }
            }

            // transform back animation
            if (playerSkillScript.currentEnergy <= 0 && (playerSkillScript.sr.sprite.name.Contains("Red") ||
                                                         playerSkillScript.sr.sprite.name.Contains("Blue") ||
                                                         playerSkillScript.sr.sprite.name.Contains("Green")))
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedTransformBack", true);
                    animator.SetBool("IsRed", false);
                    animator.SetBool("RedOnGround", false);
                }

                if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueTransformBack", true);
                    animator.SetBool("IsBlue", false);
                    animator.SetBool("BlueOnGround", false);
                }

                if (animator.GetBool("IsGreen"))
                {
                    animator.SetBool("GreenTransformBack", true);
                    animator.SetBool("IsGreen", false);
                    animator.SetBool("GreenOnGround", false);
                }
            }
        }
    }
}