using System;
using Unity.Mathematics;
using UnityEngine;

namespace Enemy
{
    public class BossRun : StateMachineBehaviour
    {
        public float speed = 8.5f;
        public float attackRange = 2.5f;
        private Transform player;
        private Rigidbody2D rb;
        private Boss boss;

        public bool isAttacking;

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

            if (Math.Abs(target.x - rb.position.x) < 6 && Math.Abs(target.y - rb.position.y) < 2 && !boss.stop)
            {
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);

                if (Vector2.Distance(player.position, rb.position) < attackRange)
                {
                    animator.SetTrigger("Attack");
                    //Instantiate(effect, rb.position, quaternion.identity);
                    isAttacking = true;
                }
                else
                {
                    isAttacking = false;
                    rb.MovePosition(newPos);
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger("Attack");
        }
    }
}