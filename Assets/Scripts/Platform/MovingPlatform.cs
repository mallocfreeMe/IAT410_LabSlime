using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platform
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector2 velocity;
        private bool reverse;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Ground"))
            {
                reverse = !reverse;
            }

            if (other.collider.gameObject.CompareTag("Player"))
            {
                other.collider.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                other.collider.transform.SetParent(null);
            }
        }

        private void FixedUpdate()
        {
            if (!reverse)
            {
                transform.position += (Vector3) (velocity * Time.deltaTime);
            }
            else
            {
                transform.position += (Vector3) (-velocity * Time.deltaTime);
            }
        }
    }
}