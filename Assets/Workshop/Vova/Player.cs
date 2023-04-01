using System;
using UnityEngine;

namespace Workshop.Vova
{
    public class Player : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot(Vector2 direction, float power)
        {
            Debug.Log("Force added");
            var dirToShoot = new Vector3(direction.x * -1, 0, direction.y * -1).normalized;
            Debug.Log("Force added = " + dirToShoot);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(dirToShoot * power * 10, ForceMode.VelocityChange);
        }
    }
}