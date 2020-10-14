using System.Collections;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI textDisplay;
        public string[] sentences;
        public int index;
        public float typingSpeed;
        public GameObject continueButton;
        public GameObject dialogueBox;

        private void Start()
        {
            dialogueBox.SetActive(true);
            continueButton.SetActive(true);
            StartCoroutine(Type());
        }

        private void Update()
        {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }

            if (index == sentences.Length - 1 && continueButton.activeSelf == false)
            {
                dialogueBox.SetActive(false);
            }
        }

        IEnumerator Type()
        {
            foreach (char letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void NextSentence()
        {
            continueButton.SetActive(false);
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