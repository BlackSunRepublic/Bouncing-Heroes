using System;
using UnityEngine;

namespace Workshop
{
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerAimDrawer : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetNewPointToLine(Vector2 startPoint, Vector2 endPoint)
        {
            DeleteLine();
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);
        }

        public void DeleteLine()
        {
            _lineRenderer.positionCount = 0;
        }
    }
}