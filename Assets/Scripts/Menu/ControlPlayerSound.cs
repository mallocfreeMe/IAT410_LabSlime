using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayerSound : MonoBehaviour
{

    private AudioSource[] arrayMusic;
    private AudioSource music1;
    private AudioSource music2;
    private AudioSource music3;
    private AudioSource music4;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        arrayMusic = gameObject.GetComponents<AudioSource>();
        music1 = arrayMusic[0];
        music2 = arrayMusic[1];
        music3 = arrayMusic[2];
        music4 = arrayMusic[3];
    }

    // Update is called once per frame
    void Update()
    {
        music1.volume = slider.value;
        music2.volume = slider.value;
        music3.volume = slider.value;
        music4.volume = slider.value;
    }
}
