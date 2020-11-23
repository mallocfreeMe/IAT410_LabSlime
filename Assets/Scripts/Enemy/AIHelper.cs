using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class AIHelper : MonoBehaviour
    {
        public AIPath aiPath;

        // Update is called once per frame
        private void Update()
        {
            if (aiPath.enabled)
            {
                if (aiPath.desiredVelocity.x >= 0.01f)
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else if (aiPath.desiredVelocity.x <= -0.01f)
                {
                    transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                }
            }
        }
    }
}