using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerSkill : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        private int speed = 20;
        private int eatingRange = 2;
        private bool isEating;
        private GameObject enemyBeEaten;
        private int animationLastSeconds = 1;
        private Vector2 right;
        private RaycastHit2D hasEnemyRight;
        private int detectDistance = 5;
        private int playerLayer = 11;
        // private int enemyLayer = 12;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            right = transform.TransformDirection(Vector2.right) * detectDistance;
            Debug.DrawRay(transform.position, right, Color.green);

            hasEnemyRight = Physics2D.Raycast(transform.position, right, detectDistance, playerLayer);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Debug.Log(hasEnemyRight.collider.gameObject.name);
                if (hasEnemyRight.collider)
                {
                    Debug.Log("collision is detected");
                    enemyBeEaten = hasEnemyRight.collider.gameObject;
                    isEating = true;
                }
            }

            if (isEating)
            {
                Vector2 target = hasEnemyRight.collider.gameObject.transform.position;
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
                rb.MovePosition(newPos);

                if (Vector2.Distance(target, rb.position) < eatingRange)
                {
                    StartCoroutine(PlayEatingAnimation());
                    isEating = false;
                }
            }
        }

        private IEnumerator PlayEatingAnimation()
        {
            animator.SetTrigger("Eat");
            yield return new WaitForSeconds(animationLastSeconds);
            Destroy(enemyBeEaten);
        }
    }
}