﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Platform;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Image[] hearts;

        public int health;
        public int numOfHearts;
        public Sprite fullHeart;
        public Sprite emptyHeart;
        public float invincibleTimeAfterHurt = 2.0f;
        private Animator animator;
        private PlayerSkill _playerSkillScript;
        private bool _isInvincible;
        private PlayerDash _playerDashScript;

        public GameObject levelLoader;
        public Dialogue.Dialogue dialogueManager;
        private Transform respawnPos;

        public AudioSource audioSource;
        public AudioSource audioSource2;

        private void Start()
        {
            animator = GetComponent<Animator>();
            _playerSkillScript = GetComponent<PlayerSkill>();
            _playerDashScript = GetComponent<PlayerDash>();
        }

        private void Update()
        {
            if (health >= numOfHearts)
            {
                health = numOfHearts;
            }

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }

                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }

            if (health == 0)
            {
                levelLoader.SetActive(true);
                levelLoader.GetComponent<LevelLoader>().timeCounter = 1;

                _playerSkillScript.currentEnergy = 0;
                if (respawnPos != null)
                {
                    transform.position = respawnPos.position;
                    levelLoader.GetComponent<LevelLoader>().disable = true;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
                }

                health = numOfHearts;
                var enemyLayer = LayerMask.NameToLayer("Enemy");
                var magmaLayer = LayerMask.NameToLayer("Magma");
                var trapLayer = LayerMask.NameToLayer("Trap");
                var playerLayer = LayerMask.NameToLayer("Player");
                Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(magmaLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(trapLayer, playerLayer, false);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // when player collide with red or blue enemies or green enemies
            if ((other.gameObject.CompareTag("red") || other.gameObject.CompareTag("blue") ||
                 other.gameObject.CompareTag("green")) &&
                !_playerSkillScript.isEating && !_isInvincible && _playerDashScript.direction == 0)
            {
                _isInvincible = true;
                health--;
                audioSource.Play();
                StartCoroutine(HurtBlinker());
            }

            // when player collide with magma or trap 
            if ((other.gameObject.name == "Magma" ||
                 other.gameObject.name == "Trap") && !_isInvincible && _playerDashScript.direction == 0)
            {
                _isInvincible = true;
                health--;
                audioSource.Play();
                StartCoroutine(HurtBlinkerForEnvironment(other.gameObject.name));
            }

            // when player collide with power up items
            if (other.gameObject.CompareTag("Heart"))
            {
                audioSource2.Play();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CheckPoint"))
            {
                respawnPos = other.transform;
            }

            if (other.gameObject.GetComponent<BossBullet>() && !_playerSkillScript.isEating &&
                _playerDashScript.direction == 0)
            {
                Destroy(other.gameObject);
                if (!_isInvincible)
                {
                    _isInvincible = true;
                    health--;
                    audioSource.Play();
                    StartCoroutine(HurtBlinker());
                }
            }

            if (other.gameObject.GetComponent<Boss>() && !_playerSkillScript.isEating && !_isInvincible &&
                _playerDashScript.direction == 0)
            {
                _isInvincible = true;
                health--;
                audioSource.Play();
                StartCoroutine(HurtBlinker());
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!_playerSkillScript.isEating && _playerDashScript.direction == 0)
            {
                Destroy(other);
                if (!_isInvincible)
                {
                    _isInvincible = true;
                    health--;
                    audioSource.Play();
                    StartCoroutine(HurtBlinker());
                }
            }
        }

        private IEnumerator HurtBlinker()
        {
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            animator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            animator.SetLayerWeight(1, 0);
            _isInvincible = false;
        }

        private IEnumerator HurtBlinkerForEnvironment(string layerName)
        {
            int enemyLayer = LayerMask.NameToLayer(layerName);
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
            animator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            animator.SetLayerWeight(1, 0);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            animator.SetLayerWeight(1, 0);
            _isInvincible = false;
        }
    }
}