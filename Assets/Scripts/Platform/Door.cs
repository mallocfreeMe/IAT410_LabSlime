using System;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
    public class Door : MonoBehaviour
    {
        public bool open;
        private float _timerCounter = 1;
        private bool _enter;

        private void Update()
        {
            if (_enter)
            {
                _timerCounter -= Time.deltaTime;
                // Debug.Log(_timerCounter);
                if (_timerCounter <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (open)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    _enter = true;
                }
            }
        }
    }
}