using System;
using UnityEngine;

namespace Enemy
{
    public class BossBullet : MonoBehaviour
    {
        private const float Speed = 8f;
        private Rigidbody2D _rb;
        private GameObject _player;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
            if (_player.transform.position.x > _rb.position.x && _player.transform.position.y > _rb.position.y)
            {
                transform.eulerAngles = new Vector3(180, -180, 0);
            }
            else if (_player.transform.position.x > _rb.position.x && _player.transform.position.y < _rb.position.y)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (_player.transform.position.x < _rb.position.x && _player.transform.position.y > _rb.position.y)
            {
                transform.eulerAngles = new Vector3(180, 0, 0);
            }
            else if (_player.transform.position.x < _rb.position.x && _player.transform.position.y < _rb.position.y)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            Vector2 target = new Vector2(_player.transform.position.x, _player.transform.position.y);
            Vector2 playerPos = Vector2.MoveTowards(_rb.position, target, Speed * Time.deltaTime);
            _rb.MovePosition(playerPos);
        }
    }
}