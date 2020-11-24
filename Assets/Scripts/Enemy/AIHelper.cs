using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class AIHelper : MonoBehaviour
    {
        public AIPath aiPath;
        public float size;

        // Update is called once per frame
        private void Update()
        {
            if (aiPath.enabled)
            {
                if (aiPath.desiredVelocity.x >= 0.01f)
                {
                    transform.localScale = new Vector3(size, size, size);
                }
                else if (aiPath.desiredVelocity.x <= -0.01f)
                {
                    transform.localScale = new Vector3(-size, size, size);
                }
            }
        }
    }
}