using Enemy;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        private GameObject instantiateBullet;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            instantiateBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(instantiateBullet, 1);
        }
    }
}