using System;
using Pathfinding;
using UnityEngine;

namespace Dialogue
{
    public class Computer : MonoBehaviour
    {
        public GameObject computerUI;
        public GameObject ai;

        public bool open;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!open)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    computerUI.SetActive(true);
                    open = true;
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && open)
            {
                computerUI.SetActive(false);
                ai.GetComponent<AIPath>().enabled = true;
            }
        }
    }
}
