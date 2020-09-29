using System;
using System.Collections;
using UnityEngine;
using Player;
using Unity.Mathematics;

namespace Enemy
{
    public class ParticleEffect : MonoBehaviour
    {
        public GameObject effect;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                Instantiate(effect, transform.position, quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}