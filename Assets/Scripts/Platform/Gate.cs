using System;
using Dialogue;
using UnityEngine;

namespace Platform
{
    public class Gate : MonoBehaviour
    {
        private Animator _animator;
        public GameObject key;
        private Switch _keyScript;
        private BoxCollider2D _collider2D;

        public Computer computerUI;
        private bool _special;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _keyScript = key.GetComponent<Switch>();
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (!computerUI)
            {
                if (_keyScript.isPicked)
                {
                    _animator.SetTrigger("Open");
                    _keyScript.isPicked = false;
                    _collider2D.enabled = false;
                }
            }
            else
            {
                if (computerUI.open && !_special)
                {
                    _animator.SetTrigger("Open");
                    _special = true;
                    _collider2D.enabled = false;
                }
            }
        }
    }
}