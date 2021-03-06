﻿using System;
using System.Collections;
using UnityEngine;
using Player;
using Unity.Mathematics;

namespace Enemy
{
    public class ParticleEffect : MonoBehaviour
    {
        public GameObject effect;
        public int health = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                Instantiate(effect, transform.position, quaternion.identity);
                health--;
                if (health <= 0)
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                }
            }
            
            if (other.gameObject.GetComponent<PlayerDash>())
            {
                if (other.gameObject.GetComponent<PlayerDash>().direction != 0)
                {
                    Instantiate(effect, transform.position, quaternion.identity);
                    health--;
                    if (health <= 0)
                    {
                        Destroy(gameObject);
                    } 
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerDash>())
            {
                if (other.gameObject.GetComponent<PlayerDash>().direction != 0)
                {
                    Instantiate(effect, transform.position, quaternion.identity);
                    health--;
                    if (health <= 0)
                    {
                        Destroy(gameObject);
                    } 
                }
            }
        }
    }
}