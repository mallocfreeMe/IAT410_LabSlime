using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyRespawn : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject enemy;
        private float _timeCounter = 5;

        private void Update()
        {
            if (enemy == null)
            {
                _timeCounter -= Time.deltaTime;
                if (_timeCounter <= 0)
                {
                    enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    _timeCounter = 5;
                }
            }
        
        }
    }
}