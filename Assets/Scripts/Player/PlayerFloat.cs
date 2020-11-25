using UnityEngine;

namespace Player
{
    public class PlayerFloat : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public bool isFloating;
        private double _timer = 3.5;
        
        private PlayerSkill _playerSkillScript;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerSkillScript = GetComponent<PlayerSkill>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFloating = true;
            }

            if (isFloating)
            {
                _rb.velocity = Vector2.up * 2;
                _rb.gravityScale = 0;
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _playerSkillScript.EnergyBarTimeCounter(25);
                    isFloating = false;
                    _rb.gravityScale = 2;
                    _timer = 3.5;
                }
            }
        }
    }
}
