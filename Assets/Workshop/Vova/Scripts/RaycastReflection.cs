using Unity.VisualScripting;
using UnityEngine;

namespace Workshop
{
    public class RaycastReflection : MonoBehaviour
    {

#if UNITY_EDITOR
        private void Update()
        {

            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var delta = mouse - this.transform.position;
            var ray = new Ray2D(this.transform.position, delta);

            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0f);
            if (hit)
            {
                // Get a rotation to go from our ray direction (negative, so coming from the wall),
                // to the normal of whatever surface we hit.
                var deflectRotation = Quaternion.FromToRotation(-ray.direction, hit.normal);

                // We then take that rotation and apply it to the same normal vector to basically
                // mirror that angle difference.
                Vector2 deflectDirection = deflectRotation * hit.normal * 10;

                Debug.DrawRay(hit.point, deflectDirection, Color.magenta, 0f);

                var ray2 = new Ray2D(hit.point, deflectDirection);
                var hit2 = Physics2D.Raycast(ray2.origin, ray2.direction);
                // Debug.DrawRay(ray2.origin, hit2.point, Color.green, 0f);
            }
        }
#endif
    }
}