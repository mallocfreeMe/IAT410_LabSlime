using UnityEngine;

namespace Enemy
{
    public class ShowBoss : MonoBehaviour
    {
        public GameObject boss;
        public GameObject player;

        private void Awake()
        {
            boss.SetActive(true);
            player.SetActive(true);
        }
    }
}
