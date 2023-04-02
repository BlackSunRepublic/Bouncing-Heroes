using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovementPath MyPath;
    public float speed = 1f;
    public float distanceRotate = .1f;
    private IEnumerator<Transform> pointInPath;
    // Start is called before the first frame update
    void Start()
    {
        if (MyPath == null)
        {
            return;
        }
        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();
        if (pointInPath.Current == null)
        {
            return;
        }
        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        var distance = transform.position - pointInPath.Current.position;
        var distanceSqure = (distance).sqrMagnitude;
        transform.up = - distance / distanceSqure;
        if (distanceSqure < distanceRotate * distanceRotate)
        {
            pointInPath.MoveNext();
        }
    }
}
