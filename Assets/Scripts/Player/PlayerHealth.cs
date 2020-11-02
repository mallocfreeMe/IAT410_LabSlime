using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public Image[] hearts;
        public Collider2D[] myColliderCollider2Ds;
        
        public int health;
        public int numOfHearts;
        public Sprite fullHeart;
        public Sprite emptyHeart;
        public float invincibleTimeAfterHurt = 2.0f;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (health > numOfHearts )
            {
                health = numOfHearts;
            }
            
            if (health == 0)
            {
                // load current scene again
                StartCoroutine(PlayDeathAnimation());
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                var enemyLayer = LayerMask.NameToLayer("Enemy");
                var magmaLayer = LayerMask.NameToLayer("Magma");
                var trapLayer = LayerMask.NameToLayer("Trap");
                var playerLayer = LayerMask.NameToLayer("Player");
                Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(magmaLayer, playerLayer, false);
                Physics2D.IgnoreLayerCollision(trapLayer, playerLayer, false);
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
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // when player collide with red or blue enemies 
            if (other.gameObject.CompareTag("red") || other.gameObject.CompareTag("blue"))
            {
                StartCoroutine(HurtBlinker());
            }
            
            // when player collide with magma or trap 
            if (other.gameObject.name == "Magma" || other.gameObject.name == "Trap")
            {
                StartCoroutine(HurtBlinkerForEnvironment(other.gameObject.name));
            }
        }
        
        private IEnumerator HurtBlinker()
        {
            int enemyLayer = LayerMask.NameToLayer("Enemy");
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, true);
            foreach (Collider2D collider2D in myColliderCollider2Ds)
            {
                collider2D.enabled = false;
                collider2D.enabled = true;
            }

            animator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            animator.SetLayerWeight(1, 0);
            foreach (Collider2D collider2D in myColliderCollider2Ds)
            {
                collider2D.enabled = true;
            }
            health--;
        }

        private IEnumerator HurtBlinkerForEnvironment(string layerName)
        {
            int enemyLayer = LayerMask.NameToLayer(layerName);
            int playerLayer = LayerMask.NameToLayer("Player");
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
            foreach (Collider2D collider2D in myColliderCollider2Ds)
            {
                collider2D.enabled = false;
            }

            animator.SetLayerWeight(1, 1);
            yield return new WaitForSeconds(invincibleTimeAfterHurt);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            animator.SetLayerWeight(1, 0);
            foreach (Collider2D collider2D in myColliderCollider2Ds)
            {
                collider2D.enabled = true;
            }
            health--;
        }

        private IEnumerator PlayDeathAnimation ()
        {
            yield return new WaitForSeconds(2);
        }
    }
}