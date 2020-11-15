using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class EnermyGFX : MonoBehaviour
    {
        public AIPath aiPath;
        public GameObject dialogueMangager;
        public GameObject button;
        
        // Update is called once per frame
        void Update()
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            if (dialogueMangager.GetComponent<Dialogue.Dialogue>().index == 1 && !button.activeSelf)
            {
                aiPath.enabled = true;
            }
        }
    }
}