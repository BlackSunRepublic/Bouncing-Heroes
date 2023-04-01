using System;
using UnityEngine;

namespace Workshop
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _maxPowerOfShoot = 3f;

        public event Action OnStop;

        private Rigidbody2D _rigidbody2D;
        private float _baseMultiplier = 1;
        private float _currentMultiplier;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _currentMultiplier = _baseMultiplier;
        }

        private void Update()
        {
            _rigidbody2D.velocity *= _currentMultiplier;
            if (_rigidbody2D.velocity.magnitude < 0.1)
            {
                OnStop?.Invoke();
            }
        }

        public void CorrectCurrentVelocityMultiplier(float filedFrictionVale)
        {
            _currentMultiplier += filedFrictionVale;
        }

        public void Shoot(Vector2 direction, float power)
        {
            _rigidbody2D.velocity = Vector2.zero;
            var inverseDir = new Vector2(direction.x, direction.y).normalized;
            _rigidbody2D.AddForce(inverseDir * (_maxPowerOfShoot * power), ForceMode2D.Impulse);
        }

        public void StopToReadyToShoot()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

    }
}