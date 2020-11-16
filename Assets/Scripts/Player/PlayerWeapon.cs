using System;
using Enemy;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        private GameObject instantiateBullet;
        
        private PlayerSkill _playerSkillScript;

        private void Start()
        {
            _playerSkillScript = GetComponent<PlayerSkill>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                _playerSkillScript.EnergyBarTimeCounter(25);
            }
        }

        private void Shoot()
        {
            instantiateBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(instantiateBullet, 1);
        }
    }
}