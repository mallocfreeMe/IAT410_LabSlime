using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Shoot : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        private Animator animator;
        private bool isCreated;
        private GameObject instantiateBullet;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Level2Smart_Attack"))
            {
                if (!isCreated)
                {
                    instantiateBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    isCreated = true;
                    Destroy(instantiateBullet, 1);
                    StartCoroutine(Wait());
                }
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(2);
            isCreated = false;
        }
    }
}
