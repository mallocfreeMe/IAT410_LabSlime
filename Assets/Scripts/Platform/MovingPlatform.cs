using System;
using UnityEngine;

namespace Platform
{
    public class MovingPlatform : MonoBehaviour
    {
        private Vector2 velocity;
        private bool moving;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                moving = true;
                other.collider.transform.SetParent((transform));
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                other.collider.transform.SetParent(null);
            }
        }

        private void FixedUpdate()
        {
            if (moving)
            {
                transform.position += (Vector3)(velocity * Time.deltaTime);
            }
        }
    }
}
