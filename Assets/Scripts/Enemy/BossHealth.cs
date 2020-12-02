using System;
using Platform;
using UnityEngine;

namespace Enemy
{
    public class BossHealth : MonoBehaviour
    {
        private float _width;
        private ParticleEffect _script;
        private float _maxHealth;
        public Door door;

        private void Start()
        {
            _width = transform.localScale.x;
            _script = GetComponentInParent<ParticleEffect>();
            _maxHealth = _script.health;
        }

        private void Update()
        {
            var scale = (1 - (_maxHealth - _script.health) / _maxHealth) * _width;
            transform.localScale = new Vector3(scale, transform.localScale.y,transform.localScale.z);

            if (_script.health == 0)
            {
                door.GetComponent<Door>().open = true;
            }
        }
    }
}
