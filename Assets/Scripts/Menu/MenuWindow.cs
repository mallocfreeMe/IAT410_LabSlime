using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuWindow : MonoBehaviour
{
    
    public void NewGame() {
        Debug.Log("The play button pressed!!");
        SceneManager.LoadScene(0);
    }

    public void Options() {
        Debug.Log("The options button pressed!!");
        SceneManager.LoadScene(2);
    }

    public void Quit() {
        Debug.Log("The Quit button pressed!!");
    }

}
