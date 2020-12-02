using System;
using Platform;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Boss : MonoBehaviour
    {
        public Transform player;
        public bool isFlipped;
        private int health = 10;

        public bool stop;

        public void LookAtPlayer()
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
               // transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                //transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground") || other.gameObject.GetComponent<Gate>() ||
                other.gameObject.name == "Trap")
            {
                stop = true;
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