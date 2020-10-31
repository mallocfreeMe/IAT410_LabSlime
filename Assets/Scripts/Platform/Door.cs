using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
     public class Door : MonoBehaviour
     {
        public int sence;
          private void OnCollisionEnter2D(Collision2D other)
          {
               SceneManager.LoadScene(sence);
          }
     }
}
