using System;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class BossHealthText : MonoBehaviour
    {
        public TextMeshPro health;

        private void Start()
        {
            health.text = GetComponentInParent<ParticleEffect>().health.ToString();
        }

        private void Update()
        {
            health.text = GetComponentInParent<ParticleEffect>().health.ToString();
        }
    }
}
