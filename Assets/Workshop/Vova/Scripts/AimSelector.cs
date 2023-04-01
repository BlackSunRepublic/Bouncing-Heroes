using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;
using Workshop;

public class AimSelector : MonoBehaviour
{
    [FormerlySerializedAs("playerMover")] [SerializeField] private PlayerShooter playerShooter;

    private Camera _camera;
    private Vector2 _startAimPoint;
    private Vector2 _currentAimPoint;
    private Vector2 _endAimPoint;
    private float _distancePower;

    private bool _isPlayerTouched = false;

    void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                var player = hit.collider.GetComponent<PlayerShooter>();
                if(player == null)
                    return;
                _isPlayerTouched = true;
                StartAim();

            }
        }

        if (Input.GetMouseButton(0) && _isPlayerTouched)
        {
            StalkAim();
        }

        if (Input.GetMouseButtonUp(0) && _isPlayerTouched)
        {
            EndAim();
            _isPlayerTouched = false;
        }

    }

    private void StartAim()
    {

// ShowPoint
// StartDrawLine
// GetStartVector2
        _startAimPoint = playerShooter.transform.position;
    }

    private void StalkAim()
    {
        Vector2 tempEnd =  _camera.ScreenToWorldPoint(Input.mousePosition);
        var tempDir = _startAimPoint - tempEnd;
        Debug.DrawRay(_startAimPoint, tempDir, Color.red, 0.05f, true);
    }

    private void EndAim()
    {
        _endAimPoint =  _camera.ScreenToWorldPoint(Input.mousePosition);
        var dir = (_startAimPoint - _endAimPoint);
        Debug.Log("direction = " + dir);

        playerShooter.Shoot(dir, 1);
//HidePoint
//GetEndVector2
//GetDistance
//
    }

}
