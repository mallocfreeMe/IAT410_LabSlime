using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class EnermyGFX : MonoBehaviour
    {
        public AIPath aiPath;

        // Update is called once per frame
        void Update()
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
            }
        }
    }
}