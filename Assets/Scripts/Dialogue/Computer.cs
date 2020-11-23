using System;
using Pathfinding;
using UnityEngine;

namespace Dialogue
{
    public class Computer : MonoBehaviour
    {
        public GameObject computerUI;
        public GameObject ai;

        private bool _open;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_open)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    computerUI.SetActive(true);
                    _open = true;
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _open)
            {
                computerUI.SetActive(false);
                ai.GetComponent<AIPath>().enabled = true;
            }
        }
    }
}
