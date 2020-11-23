using System;
using UnityEngine;

namespace Platform
{
    public class Key : MonoBehaviour
    {
        public bool isPicked;
            
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isPicked = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
}
