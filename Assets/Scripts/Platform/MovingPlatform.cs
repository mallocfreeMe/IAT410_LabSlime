using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platform
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector2 velocity;
        public bool allowReverse;
        private bool moving;
        private bool reverse;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                moving = true;
                other.collider.transform.SetParent(transform);
            }
            else
            {
                reverse = !reverse; 
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                other.collider.transform.SetParent(null);
                moving = false;
            }
        }

        private void FixedUpdate()
        {
            if (moving && !reverse)
            {
                transform.position += (Vector3) (velocity * Time.deltaTime);
            }
            
            if (moving && allowReverse && reverse)
            {
                transform.position += (Vector3) (-velocity * Time.deltaTime);
            }
        }
    }
}