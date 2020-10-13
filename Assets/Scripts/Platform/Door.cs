using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
     public class Door : MonoBehaviour
     {
          private void OnCollisionEnter2D(Collision2D other)
          {
               SceneManager.LoadScene(5);
          }
     }
}
