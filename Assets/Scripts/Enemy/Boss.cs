using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Boss : MonoBehaviour
    {
        public Transform player;
        public bool isFlipped = false;
        private int health = 10;

        public void LookAtPlayer()
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.GetComponent<Bullet>())
            {
                Debug.Log(health);
                health--;
            }
        }

        private void Update()
        {
            if (health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}