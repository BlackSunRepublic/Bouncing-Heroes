using System;
using UnityEngine;

namespace Workshop
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerMover), typeof(PlayerAimDrawer))]
    public class Player : MonoBehaviour
    {
        public event Action OnPlayerStop;
        public bool IsPlayerMove { get => _isPlayerMove;
            private set => _isPlayerMove = value;
        }

        private PlayerAimDrawer _playerAimDrawer;
        private PlayerMover _playerMover;
        private bool _isPlayerMove = false;
        private Vector2 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;

            _playerMover = GetComponent<PlayerMover>();
            _playerAimDrawer = GetComponent<PlayerAimDrawer>();

            _playerMover.OnStop += AfterPlayerStop;
        }

        private void Start()
        {
            GameManager.instance.SetPlayerFromLevel(this);
        }

        public void Shoot(Vector2 direction, float power)
        {
            IsPlayerMove = true;
            DeleteAimLine();
            _playerMover.Shoot(direction, power);
        }

        public void DrawAimLine(Vector2 startPoint, Vector2 endPoint)
        {
            _playerAimDrawer.SetNewPointToLine(startPoint, endPoint);
        }

        private void DeleteAimLine()
        {
            _playerAimDrawer.DeleteLine();
        }

        private void AfterPlayerStop()
        {
            OnPlayerStop?.Invoke();
            IsPlayerMove = false;
        }

        private void OnDestroy()
        {
            _playerMover.OnStop -= AfterPlayerStop;
        }
    }
}