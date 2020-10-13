using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{

    public AudioSource src;
    public int musicState;

    // Start is called before the first frame update
    void Start()
    {
        musicState = 0; // The music is off
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicButton() 
    {
        if (musicState == 0) {
            src.Play();
            musicState = 1;
        }

        else {
            src.Pause();
            musicState = 0;
        }
    }


    // void Awake() {
    //     DontDestroyOnLoad(transform.gameObject);
    // }
}
