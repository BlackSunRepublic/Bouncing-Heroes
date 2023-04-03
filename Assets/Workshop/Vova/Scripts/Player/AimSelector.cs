using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using Workshop;

[RequireComponent(typeof(Player))]
public class AimSelector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _maxAimDistance = 3f;

    private Camera _camera;
    private Vector2 _startAimPoint;
    private Vector2 _currentAimPoint;
    private Vector2 _endAimPoint;
    private float _distancePower;
    private bool _isPressed = false;
    private float _powerMultiplier = 0;

    void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
    }

    void Update()
    {
        if(_player.IsPlayerMove)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //RaycastAll ??
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero/*, Mathf.Infinity, 0*/);
            if (hit.collider != null)
            {
                var player = hit.collider.GetComponent<Player>();
                if(player == null)
                    return;
                _isPressed = true;
                StartAim();
            }
        }

        if (_isPressed)
        {
            StalkAim();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && _isPressed)
        {
            _isPressed = false;
            EndAim();
        }

    }

    private void StartAim()
    {
        _startAimPoint = _player.transform.position;
    }

    private void StalkAim()
    {
        DrawLine(_startAimPoint, _player.transform.position);
        PullPlayer();
        RotatePlayer();
    }

    private void PullPlayer()
    {
        Vector2 mousePos =  _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mousePos, _startAimPoint) > _maxAimDistance)
        {
            _player.transform.position = _startAimPoint + (mousePos - _startAimPoint).normalized * _maxAimDistance;
            _powerMultiplier = 1;
        }
        else
        {
            _player.transform.position = mousePos;
            _powerMultiplier = Vector2.Distance(mousePos, _startAimPoint)/_maxAimDistance;
        }
    }

    private void RotatePlayer()
    {
        Vector2 direction = (_startAimPoint - (Vector2)_player.transform.position).normalized;
        _player.transform.up = direction;
    }

    private void DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        _player.DrawAimLine(startPoint, endPoint);

        Vector2 mousePos =  _camera.ScreenToWorldPoint(Input.mousePosition);
        var tempDir = _startAimPoint - mousePos;
        Debug.DrawRay(_startAimPoint, tempDir, Color.red);
    }

    private void EndAim()
    {
        _endAimPoint =  _camera.ScreenToWorldPoint(Input.mousePosition);
        var dir = (_startAimPoint - _endAimPoint);
        _player.Shoot(dir, _powerMultiplier);
    }

}
