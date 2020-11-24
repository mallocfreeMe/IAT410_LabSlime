using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Platform
{
    public class LevelLoader : MonoBehaviour
    {
        public Image[] hearts;
        public float timeCounter = 1;
        public GameObject dialogueManager;

        public bool disable;

        private void Start()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= 0)
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].gameObject.SetActive(true);
                }

                if (!disable)
                {
                    dialogueManager.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }
}