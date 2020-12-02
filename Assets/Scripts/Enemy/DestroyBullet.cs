using System;
using UnityEngine;

namespace Enemy
{
    public class DestroyBullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<BossBullet>())
            {
                Destroy(other.gameObject);
            }
        }
    }
}