using System;
using UnityEngine;

namespace Workshop
{
    public class PlayerMover : MonoBehaviour
    {
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
            _rigidbody2D.velocity = _rigidbody2D.velocity * _currentMultiplier;
        }

        public void CorrectCurrentVelocityMultiplier(float filedFrictionVale)
        {
            _currentMultiplier += filedFrictionVale;
        }
    }
}