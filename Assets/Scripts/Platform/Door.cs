using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platform
{
     public class Door : MonoBehaviour
     {
          private void OnCollisionEnter2D(Collision2D other)
          {
               if (other.collider.gameObject.CompareTag("Player"))
               {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
               }
          }
     }
}
