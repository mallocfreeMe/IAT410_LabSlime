using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private PlayerController playerControllerScript;
        private PlayerSkill playerSkillScript;
        private Animator animator;

        void Start()
        {
            playerControllerScript = GetComponent<PlayerController>();
            playerSkillScript = GetComponent<PlayerSkill>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // move animations 
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetBool("RedIsRunning", true);
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetBool("BlueIsRunning", true);
                }
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue"))
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
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue"))
                {
                    animator.SetBool("IsRunning", false);
                }
            }

            // jump animations 
            if (playerControllerScript.isGrounded && Input.GetKeyDown(KeyCode.W))
            {
                if (animator.GetBool("IsRed"))
                {
                    animator.SetTrigger("RedJump");
                }
                else if (animator.GetBool("IsBlue"))
                {
                    animator.SetTrigger("BlueJump");
                }
                else if (!animator.GetBool("IsRed") && !animator.GetBool("IsBlue"))
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
                else
                {
                    //animator.SetTrigger("Jump");
                }
            }
            
            // dash animation for red 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("RedDash");
            }

            // transform back animation
            if (playerSkillScript.currentEnergy <= 0 && (playerSkillScript.sr.sprite.name.Contains("Red") || playerSkillScript.sr.sprite.name.Contains("Blue")))
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
            }
        }
    }
}