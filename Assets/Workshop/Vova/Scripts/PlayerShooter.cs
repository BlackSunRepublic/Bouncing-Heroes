using System;
using UnityEngine;

namespace Workshop
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private bool _isSelected = true;

        private Rigidbody2D _rigidbody2d;

        private void Awake()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector2 direction, float power)
        {
            _rigidbody2d.velocity = Vector2.zero;
            var inverseDir = new Vector2(direction.x, direction.y).normalized;
            _rigidbody2d.AddForce(inverseDir * 5, ForceMode2D.Impulse);
        }
    }
}