using System;
using UnityEngine;

namespace Platform
{
    public class Gate : MonoBehaviour
    {
        private Animator _animator;
        public GameObject key;
        private Switch _keyScript;
        private BoxCollider2D _collider2D;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _keyScript = key.GetComponent<Switch>();
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (_keyScript.isPicked)
            {
                _animator.SetTrigger("Open");
                _keyScript.isPicked = false;
                _collider2D.enabled = false;
            }
        }
    }
}