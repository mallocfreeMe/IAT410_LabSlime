using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class optionWindow : MonoBehaviour
{
    public void Music() {
        Debug.Log("The music button pressed!!");
        // SceneManager.LoadScene(0);
    }

    public void Bright() {
        Debug.Log("The bright button pressed!!");
    }

    public void Back() {
        Debug.Log("The Back button pressed!!");
        SceneManager.LoadScene(1);
    }
}
