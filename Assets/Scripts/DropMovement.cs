using System;
using UnityEngine;
using Random = System.Random;

public class DropMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _target;
    public float speed = 2.5f;
    public int radius = 100;
    public bool dropped = false;

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (dropped)
        {
            Vector3.MoveTowards(_transform.position, _target, speed);

            if (Vector3.Distance(_transform.position, _target) < 0.5f)
                dropped = false;
        }
    }

    public void Drop()
    {
        Debug.Log("Dropped");
        int randomDegree = new Random().Next(0, 360);
        int randomRadius = new Random().Next(0, radius);
        float y = (float) Math.Sin(randomDegree) * randomRadius;
        float x = (float) Math.Cos(randomDegree) * randomRadius;
        _target = new Vector3(x, y, _transform.position.z);
        Debug.Log(_target.ToString());
        dropped = true;
    }
}