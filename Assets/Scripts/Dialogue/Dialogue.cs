using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        [Header("Dialogue Settings")] public TextMeshProUGUI textDisplay;
        public string[] sentences;
        public int index;
        public float typingSpeed;
        public GameObject dialogueBox;
        public GameObject dialogueText;
        public GameObject continueText;

        [Header("Pause Settings")] public GameObject player;
        public GameObject enemies;
        private List<GameObject> enemyList = new List<GameObject>();

        // this method was taken from https://forum.unity.com/threads/finding-all-children-of-object.453466/
        private void AddDescendantsWithTag(Transform parent, string tag, List<GameObject> list)
        {
            foreach (Transform child in parent)
            {
                if (child.gameObject.CompareTag(tag))
                {
                    list.Add(child.gameObject);
                }

                AddDescendantsWithTag(child, tag, list);
            }
        }

        private void Start()
        {
            // pause all the enemies
            if (enemies != null)
            {
                AddDescendantsWithTag(enemies.transform, "red", enemyList);
                foreach (GameObject enemy in enemyList)
                {
                    enemy.SetActive(false);
                }
            }

            // disable player's components
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerAnimationManager>().enabled = false;
            player.GetComponent<PlayerSkill>().enabled = false;

            // set all the dialogue objects to active
            dialogueBox.SetActive(true);
            continueText.SetActive(true);
            dialogueText.SetActive(true);
            StartCoroutine(Type());
        }

        private void Update()
        {
            if (textDisplay.text == sentences[index])
            {
                continueText.SetActive(true);
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    NextSentence();
                }
            }

            if (index == sentences.Length - 1 && continueText.activeSelf == false)
            {
                dialogueBox.SetActive(false);

                // show all the enemies once dialogue box is gone
                if (enemyList.Count > 0)
                {
                    foreach (GameObject enemy in enemyList)
                    {
                        if (enemy != null)
                        {
                            enemy.SetActive(true);
                        }
                    }
                }

                // allow player to move again 
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<PlayerAnimationManager>().enabled = true;
                player.GetComponent<PlayerSkill>().enabled = true;
            }
        }

        private IEnumerator Type()
        {
            foreach (char letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void NextSentence()
        {
            continueText.SetActive(false);
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplay.text = "";
            }
        }
    }
}