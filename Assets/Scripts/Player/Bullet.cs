using System;
using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public Rigidbody2D rb;
        public bool isPlayer;
    
        // Start is called before the first frame update
        void Start()
        {
            if (isPlayer)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    rb.velocity = transform.right * speed * -1;
                }
                else
                {
                    rb.velocity = transform.right * speed;
                }
            }
            else
            {
                rb.velocity = transform.right * speed * -1;
            }
        }
    }
}
