using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class BossShoot : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        public GameObject bossSkillPrefab;
        private Animator _animator;
        private bool _isCreated;
        private GameObject _instantiateBullet;

        private ParticleEffect _particleEffectScript;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _particleEffectScript = GetComponent<ParticleEffect>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Level2Boss_Attack"))
            {    
                // action one 
                if (_particleEffectScript.health > 6)
                {
                    if (!_isCreated)
                    {
                        _instantiateBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        _isCreated = true;
                        Destroy(_instantiateBullet, 3);
                        StartCoroutine(Wait());
                    }
                }
                else
                {
                    // action two
                    if (!_isCreated)
                    {
                        _instantiateBullet = Instantiate(bossSkillPrefab, firePoint.position, firePoint.rotation);
                        _isCreated = true;
                        Destroy(_instantiateBullet, 4);
                        StartCoroutine(Wait2());
                    }
                }
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            _isCreated = false;
        }
        
        IEnumerator Wait2()
        {
            yield return new WaitForSeconds(4);
            _isCreated = false;
        }
    }
}
