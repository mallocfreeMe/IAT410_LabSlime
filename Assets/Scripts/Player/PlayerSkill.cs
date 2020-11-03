using System;
using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerSkill : MonoBehaviour
    {
        // consume ability 
        private Rigidbody2D rb;
        private Animator animator;
        private int speed = 20;
        private int eatingRange = 2;
        public bool isEating;
        private GameObject enemyBeEaten;
        private int animationLastSeconds = 1;
        private Vector2 right;
        private RaycastHit2D hasEnemyRight;
        private int detectDistance = 5;
        private int playerLayer = 11;
        public bool enemyIsRed;
        public bool enemyIsBlue;
        private bool allowToUseConsume;

        // Energy bar time counter 
        public GameObject energyBar;
        private EnergyBar energyBarScript;
        public double currentEnergy = 100;
        private int maxEnergy = 100;
        public SpriteRenderer sr;

        // red skill
        private Weapon weapon;

        private void Start()
        {
            allowToUseConsume = true;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            energyBarScript = energyBar.GetComponent<EnergyBar>();
            sr = GetComponent<SpriteRenderer>();
            weapon = GetComponent<Weapon>();
        }

        private void FixedUpdate()
        {
            right = transform.TransformDirection(Vector2.right) * detectDistance;
            Debug.DrawRay(transform.position, right, Color.green);

            hasEnemyRight = Physics2D.Raycast(transform.position, right, detectDistance, playerLayer);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S) && allowToUseConsume)
            {
                if (hasEnemyRight.collider && !hasEnemyRight.collider.gameObject.CompareTag("Boss"))
                {
                    enemyBeEaten = hasEnemyRight.collider.gameObject;
                    isEating = true;
                }
            }

            if (isEating)
            {
                Vector2 target = hasEnemyRight.collider.gameObject.transform.position;
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
                rb.MovePosition(newPos);
                
                // disable the patrol behaviour
                if (hasEnemyRight.collider.gameObject.GetComponent<Patrol>())
                {
                    hasEnemyRight.collider.gameObject.GetComponent<Patrol>().enabled = false;
                }

                if (hasEnemyRight.collider.gameObject.CompareTag("red"))
                {
                    enemyIsRed = true;
                }
                else if (hasEnemyRight.collider.gameObject.CompareTag("blue"))
                {
                    enemyIsBlue = true;
                }

                if (Vector2.Distance(target, rb.position) < eatingRange)
                {
                    StartCoroutine(PlayEatingAnimation());
                    isEating = false;
                }
            }

            if (sr.sprite.name.Contains("Red") || sr.sprite.name.Contains("Blue"))
            {
                allowToUseConsume = false;
                energyBar.SetActive(true);
                energyBarScript.SetMaxEnergy(maxEnergy);
                EnergyBarTimeCounter(5 * Time.deltaTime);

                if (currentEnergy <= 0)
                {
                    energyBar.SetActive(false);
                }

                weapon.enabled = true;
            }
            else
            {
                currentEnergy = maxEnergy;
                weapon.enabled = false;
                allowToUseConsume = true;
            }
        }

        private IEnumerator PlayEatingAnimation()
        {
            animator.SetTrigger("Eat");
            animator.SetBool("IsRunning", false);
            if (enemyIsRed)
            {
                enemyIsRed = false;
                animator.SetBool("IsRed", true);
                animator.SetBool("RedTransformBack", false);
                animator.SetBool("BlueTransformBack", false);
            }
            else if (enemyIsBlue)
            {
                enemyIsBlue = false;
                animator.SetBool("IsBlue", true);
                animator.SetBool("RedTransformBack", false);
                animator.SetBool("BlueTransformBack", false);
            }

            yield return new WaitForSeconds(animationLastSeconds);
            Destroy(enemyBeEaten);
        }

        private void EnergyBarTimeCounter(float energyConsumedRate)
        {
            currentEnergy -= energyConsumedRate;
            energyBarScript.SetEnergy((float) currentEnergy);
        }
    }
}