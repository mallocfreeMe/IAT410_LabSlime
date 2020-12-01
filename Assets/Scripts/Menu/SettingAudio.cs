using UnityEngine;

public class SettingAudio : MonoBehaviour
{

    private static readonly string BGMPref = "BGMPref";
    public AudioSource bgmsound;
    private float backgroundFloat;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {

        backgroundFloat = PlayerPrefs.GetFloat(BGMPref);

        bgmsound.volume = backgroundFloat;
        
    }
}
