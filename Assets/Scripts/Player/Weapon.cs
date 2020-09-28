using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
    
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
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
