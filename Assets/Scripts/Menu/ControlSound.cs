// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ControlSound : MonoBehaviour
// {

//     public AudioSource bgmsound;
//     public Slider sd;

//     // Start is called before the first frame update
//     void Start()
//     {
//         bgmsound.volume = 0.1f;
//         sd.value = 0.1f;

//     }

//     // Update is called once per frame
//     void Update()
//     {
//     }

//     public void controlSound()
//     {
//         bgmsound.volume = sd.value;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSound : MonoBehaviour
{

    public AudioSource bgmsound;
    public Slider sd;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BGMPref = "BGMPref";
    private int firstPlayInt;
    private float backgroundFloat;


    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            backgroundFloat = 0.1f;
            bgmsound.volume = 0.1f;
            sd.value = 0.1f;
            PlayerPrefs.SetFloat(BGMPref, backgroundFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BGMPref);
            sd.value = backgroundFloat;
        }

        // bgmsound.volume = 0.1f;
        // sd.value = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void controlSound()
    {
        bgmsound.volume = sd.value;

    }

    public void SaveSoundSettings() {
        PlayerPrefs.SetFloat(BGMPref, sd.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }
}

