using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSelectButton : MonoBehaviour
{
    public Button selectButton;

    // Start is called before the first frame update
    void Start()
    {
        if (selectButton != null)
        {
            selectButton.Select();
            //print("selected button");
        }
        else
        {
            // Debug.Log ("SelectButton was null");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}