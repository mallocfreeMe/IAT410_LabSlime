using System;
using UnityEngine;

namespace Platform
{
    public class CheckPoint : MonoBehaviour
    {
        private Animator _animator;
        private bool _once = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_once)
                {
                    _animator.SetTrigger("Active");
                }
                _once = false;
            }
        }
    }
}