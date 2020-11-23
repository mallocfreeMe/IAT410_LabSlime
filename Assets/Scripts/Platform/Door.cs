using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
     public class Door : MonoBehaviour
     {
          public bool open;

          private void OnTriggerEnter2D(Collider2D other)
          {
               if (open)
               {
                    if (other.gameObject.CompareTag("Player"))
                    {
                         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
               }
          }
     }
}
