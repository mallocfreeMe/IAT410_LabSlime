﻿using UnityEngine;

namespace Enemy
{
    public class Patrol : MonoBehaviour
    {
        public float speed;

        private bool movingRight = true;

        public Transform groundDetection;

        public LayerMask whatIsGround;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfor = Physics2D.Raycast(groundDetection.position, Vector2.down, 0.5f, whatIsGround);

            if (groundInfor.collider == false)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }
}