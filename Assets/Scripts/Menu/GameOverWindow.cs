using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{

    public void PlayAgain() {
        Debug.Log("The play again button pressed!!");
        SceneManager.LoadScene(0);
    }

    public void MainMenu() {
        Debug.Log("The main menu button pressed!!");
        SceneManager.LoadScene(1);
    }

}
