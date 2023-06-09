using System;
using UnityEngine;

namespace Workshop
{
    [RequireComponent(typeof(Player))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _maxPowerOfShoot = 3f;
        [SerializeField] private float _maxPowerOfRandomRotation = 1f;
        private Player _player;

        public event Action OnStop;

        private Rigidbody2D _rigidbody2D;
        private float _baseMultiplier = 1;
        private float _currentMultiplier;
        private int _turnSide = 1;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _currentMultiplier = _baseMultiplier;
        }

        private void FixedUpdate()
        {
            if(!_player.IsPlayerMove)
                return;
            _rigidbody2D.velocity *= _currentMultiplier;
            if (_rigidbody2D.velocity.magnitude < 0.1)
            {
                OnStop?.Invoke();
                _rigidbody2D.angularDrag = 1;
                _rigidbody2D.velocity = Vector2.zero;
                _player.IsPlayerMove = false;
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

        private void OnCollisionEnter2D(Collision2D col)
        {
            MakeRandomTurn();
        }

        void MakeRandomTurn()
        {
            var turnPower = Mathf.Sqrt(_rigidbody2D.velocity.magnitude) / 3;
            turnPower = Mathf.Clamp(turnPower, -_maxPowerOfRandomRotation, _maxPowerOfRandomRotation) * _turnSide;
            _rigidbody2D.angularVelocity = 0;
            _rigidbody2D.AddTorque(turnPower, ForceMode2D.Impulse);
            _turnSide *= -1;
        }
    }
}