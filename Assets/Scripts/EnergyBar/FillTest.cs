using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTest : MonoBehaviour
{

    public Image ebr;
    public Image ebb;
    public Image ebg;
    private bool redStart = false;
    private bool blueStart = false;
    private bool greenStart = false;

    // Start is called before the first frame update
    void Start()
    {
        ebr.fillAmount = 0;
        ebb.fillAmount = 0;
        ebg.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // red
        if (Input.GetKeyDown(KeyCode.R) )
        {
            ebr.fillAmount = 1;
            redStart = true;
        }
        if (Input.GetKeyDown(KeyCode.H) )
        {
            ebr.fillAmount = 0;
            redStart = false;
            ebb.fillAmount = 0;
            blueStart = false;
            ebg.fillAmount = 0;
            greenStart = false;
        }

        // blue
        if (Input.GetKeyDown(KeyCode.B) )
        {
            ebb.fillAmount = 1;
            blueStart = true;
        }

        //green
        if (Input.GetKeyDown(KeyCode.G) )
        {
            ebg.fillAmount = 1;
            greenStart = true;
        }



        if (redStart)
        {
            ebr.fillAmount -= Time.deltaTime * 0.1f;
            if(ebr.fillAmount == 0){
				redStart = false;
			}
        }

        if (blueStart)
        {
            ebb.fillAmount -= Time.deltaTime * 0.1f;
            if(ebb.fillAmount == 0){
				blueStart = false;
			}
        }

        if (greenStart)
        {
            ebg.fillAmount -= Time.deltaTime * 0.1f;
            if(ebg.fillAmount == 0){
				greenStart = false;
			}
        }
    }
}
