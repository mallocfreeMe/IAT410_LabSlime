﻿using Unity.Mathematics;
using UnityEngine;

namespace Enemy
{
    public class BossRun : StateMachineBehaviour
    {
        private float speed = 8.5f;
        private float attackRange = 2.5f;
        private Transform player;
        private Rigidbody2D rb;
        private Boss boss;
        // public GameObject effect;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            rb = animator.GetComponent<Rigidbody2D>();
            boss = animator.GetComponent<Boss>();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            boss.LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) < attackRange)
            {
                animator.SetTrigger("Attack");
                //Instantiate(effect, rb.position, quaternion.identity);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("Attack");
        }
    }
}