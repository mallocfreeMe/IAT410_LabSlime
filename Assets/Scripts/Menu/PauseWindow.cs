using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseWindow : MonoBehaviour
    {
        public void Resume() {
            Debug.Log("The resume button pressed!!");
        }

        public void MainMenu() {
            Debug.Log("The main menu button pressed!!");
            SceneManager.LoadScene(1);
        }
    }
}
