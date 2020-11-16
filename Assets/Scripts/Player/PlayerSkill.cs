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
        public bool enemyIsGreen;
        private bool allowToUseConsume;

        // Energy bar counter
        public GameObject energyBar;
        private EnergyBar energyBarScript;
        public double currentEnergy = 100;
        private int maxEnergy = 100;
        public SpriteRenderer sr;
        public bool initializeEnergy = true;

        // red color character's skill - dash move
        private PlayerDash _playerDashScript;

        // blue color character's skill - float in the air
        private PlayerFloat _playerFloatScript;

        // green color character's skill - shoot bullet
        private PlayerWeapon _playerWeaponScript;

        private void Start()
        {
            allowToUseConsume = true;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            energyBarScript = energyBar.GetComponent<EnergyBar>();
            sr = GetComponent<SpriteRenderer>();

            // red
            _playerDashScript = GetComponent<PlayerDash>();

            // blue
            _playerFloatScript = GetComponent<PlayerFloat>();

            // green 
            _playerWeaponScript = GetComponent<PlayerWeapon>();
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
                else if (hasEnemyRight.collider.gameObject.CompareTag("green"))
                {
                    enemyIsGreen = true;
                }

                if (Vector2.Distance(target, rb.position) < eatingRange)
                {
                    StartCoroutine(PlayEatingAnimation());
                    isEating = false;
                }
            }

            if ((sr.sprite.name.Contains("Red") || sr.sprite.name.Contains("Blue") ||
                 sr.sprite.name.Contains("Green")) && initializeEnergy)
            {
                allowToUseConsume = false;
                energyBar.SetActive(true);
                energyBarScript.SetMaxEnergy(maxEnergy);
                initializeEnergy = false;

                // red 
                if (sr.sprite.name.Contains("Red"))
                {
                    _playerDashScript.enabled = true;
                }

                // blue 
                if (sr.sprite.name.Contains("Blue"))
                {
                    _playerFloatScript.enabled = true;
                }
                
                // green 
                if (sr.sprite.name.Contains("Green"))
                {
                    _playerWeaponScript.enabled = true;
                }
            }
            else if ((sr.sprite.name.Contains("Red") || sr.sprite.name.Contains("Blue") ||
                      sr.sprite.name.Contains("Green")) && !initializeEnergy)
            {
                if (currentEnergy <= 0)
                {
                    energyBar.SetActive(false);
                    _playerDashScript.direction = 0;
                    _playerDashScript.enabled = false;
                    _playerFloatScript.enabled = false;
                    _playerWeaponScript.enabled = false;
                }
            }
            else if (!sr.sprite.name.Contains("Red") || !sr.sprite.name.Contains("Blue") ||
                     !sr.sprite.name.Contains("Green"))
            {
                initializeEnergy = true;
                currentEnergy = maxEnergy;
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
                animator.SetBool("GreenTransformBack", false);
            }
            else if (enemyIsBlue)
            {
                enemyIsBlue = false;
                animator.SetBool("IsBlue", true);
                animator.SetBool("RedTransformBack", false);
                animator.SetBool("BlueTransformBack", false);
                animator.SetBool("GreenTransformBack", false);
            }
            else if (enemyIsGreen)
            {
                enemyIsGreen = false;
                animator.SetBool("IsGreen", true);
                animator.SetBool("RedTransformBack", false);
                animator.SetBool("BlueTransformBack", false);
                animator.SetBool("GreenTransformBack", false);
            }

            yield return new WaitForSeconds(animationLastSeconds);
            Destroy(enemyBeEaten);
        }

        public void EnergyBarTimeCounter(float energyConsumedRate)
        {
            currentEnergy -= energyConsumedRate;
            energyBarScript.SetEnergy((float) currentEnergy);
        }
    }
}