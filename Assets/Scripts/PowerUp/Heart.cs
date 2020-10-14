using System;
using UnityEngine;
using Player;
using Unity.Mathematics;

namespace PowerUp
{
    public class Heart : MonoBehaviour
    {
        public PlayerController player;
        public GameObject effect;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                Instantiate(effect, transform.position, quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
