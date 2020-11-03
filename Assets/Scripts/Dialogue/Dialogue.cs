using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI textDisplay;
        public string[] sentences;
        public int index;
        public float typingSpeed;
        public GameObject dialogueBox;
        public GameObject dialogueText;
        public GameObject continueText;

        private void Start()
        {
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