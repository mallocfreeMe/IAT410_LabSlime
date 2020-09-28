using System;
using System.Collections;
using UnityEngine;
using Player;
using Unity.Mathematics;

namespace Enemy
{
    public class ParticleEffect : MonoBehaviour
    {
        public int health;
        public GameObject effect;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.GetComponent<PlayerController>())
            {
                Instantiate(effect, transform.position, quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}