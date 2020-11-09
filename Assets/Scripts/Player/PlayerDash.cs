using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class PlayerDash : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float dashSpeed;
        public float dashTime;
        public float startDashTime;
        public int direction;

        private PlayerSkill _playerSkillScript;
        private PlayerController _playerController;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            dashTime = startDashTime;
            _playerSkillScript = GetComponent<PlayerSkill>();
            _playerController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (direction == 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_playerController.facingRight)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = 2;
                    }
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    _playerSkillScript.EnergyBarTimeCounter(25);
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        rb.velocity = Vector2.right * dashSpeed;
                    }
                    else if(direction == 2)
                    {
                        rb.velocity = Vector2.left * dashSpeed;
                    }
                }
            }
        }
    }
}