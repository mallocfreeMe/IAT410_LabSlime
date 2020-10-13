using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuWindow : MonoBehaviour
    {
    
        public void NewGame() {
            Debug.Log("The play button pressed!!");
            SceneManager.LoadScene(3);
        }

        public void Options() {
            Debug.Log("The options button pressed!!");
            SceneManager.LoadScene(1);
        }

        public void Quit() {
            Debug.Log("The Quit button pressed!!");
        }

    }
}
