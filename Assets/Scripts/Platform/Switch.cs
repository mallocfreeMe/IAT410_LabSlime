using System;
using UnityEngine;

namespace Platform
{
    public class Switch : MonoBehaviour
    {
        public bool isPicked;
        private Animator _animator;
        private int _count = 0;

        public AudioSource playSrc;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            playSrc = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
            {
                _count++;
                if (_count == 1)
                {
                    isPicked = true;
                    _animator.SetTrigger("Open");
                    playSrc.Play();
                }
            }
        }
    }
}
