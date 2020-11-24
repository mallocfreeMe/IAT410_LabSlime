using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSound : MonoBehaviour
{

    public AudioSource bgmsound;
    public Slider sd;

    // Start is called before the first frame update
    void Start()
    {
        bgmsound.volume = 0.1f;
        sd.value = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void controlSound()
    {
        bgmsound.volume = sd.value;
    }
}
