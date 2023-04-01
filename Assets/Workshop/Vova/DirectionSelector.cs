using System;
using UnityEngine;

namespace Workshop.Vova
{
    public class DirectionSelector : MonoBehaviour
    {
        [SerializeField]
        private FloatingJoystick _joystick;

        [SerializeField]
        private Player _player;

        private Camera _cam;
        private Vector2 _direction;
        private float _power;
        private Vector2 _startPosition;


        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    var player = hit.collider.GetComponent<Player>();
                    if (player != null)
                    {
                        StartAim();
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                StalkAim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopAim();
            }
        }



        private void StartAim()
        {
            Debug.Log("Start aim");
            _startPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
        }
        private void StalkAim()
        {
            _direction = _joystick.Direction;
            var currentPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            _power = (_joystick.Direction - Vector2.zero).magnitude;
        }

        private void StopAim()
        {
            Debug.Log("Stop aim");
            Debug.Log(_direction);
            Debug.Log(_power);

            if (_player == null)
            {
                return;
            }
            _player.Shoot(_direction, _power);
        }
    }
}