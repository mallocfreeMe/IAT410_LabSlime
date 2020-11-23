using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTest : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Test red
        transRed();


        // Test green
        transGreen();


        // Test blue
        transBlue();
    }

    void transRed()
    {
        if (Input.GetKeyDown(KeyCode.R) )
        {
            animator.SetBool("IsRed", true);
        }
        if (Input.GetKeyDown(KeyCode.H) )
        {
            animator.SetBool("IsRed", false);
        } 
    }

    void transBlue()
    {
        if (Input.GetKeyDown(KeyCode.B) )
        {
            animator.SetBool("IsBlue", true);
        }
        if (Input.GetKeyDown(KeyCode.H) )
        {
            animator.SetBool("IsBlue", false);
        } 
    }

    void transGreen()
    {
        if (Input.GetKeyDown(KeyCode.G) )
        {
            animator.SetBool("IsGreen", true);
        }
        if (Input.GetKeyDown(KeyCode.H) )
        {
            animator.SetBool("IsGreen", false);
        }
    }
}
