using System;
using UnityEngine;
using Player;
using Unity.Mathematics;

namespace PowerUp
{
    public class Heart : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.CompareTag("Player"))
            {
                other.collider.gameObject.GetComponent<PlayerHealth>().health++;
                Destroy(gameObject);
            }
        }
    }
}