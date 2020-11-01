using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private PlayerSkill playerSkillScript;
        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            playerSkillScript = GetComponent<PlayerSkill>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (playerSkillScript.enemyIsRed)
                {
                    animator.SetBool("RedIsRunning", true);
                }
                else if (playerSkillScript.enemyIsBlue)
                {
                    animator.SetBool("BlueIsRunning", true);
                }
                else
                {
                    animator.SetBool("IsRunning", true);
                }
            }
            else
            {
                if (playerSkillScript.enemyIsRed)
                {
                    animator.SetBool("RedIsRunning", false);
                }
                else if (playerSkillScript.enemyIsBlue)
                {
                    animator.SetBool("BlueIsRunning", false);
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                }
            }
        }
    }
}