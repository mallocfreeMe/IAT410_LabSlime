using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class GameOverWindow : MonoBehaviour
    {

        public void PlayAgain() {
            SceneManager.LoadScene(3);
        }

        public void MainMenu() {
            SceneManager.LoadScene(0);
        }

    }
}
