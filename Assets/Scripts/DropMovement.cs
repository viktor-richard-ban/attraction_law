using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class DropMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _target;
    public float speed = 0.5f;
    public int radius = 3;
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
        int randomDegree = new Random().Next(0, 360);
        int randomRadius = new Random().Next(0, radius);
        double y = Math.Sin(randomDegree) * randomRadius;
        double x = Math.Cos(randomDegree) * randomRadius;
        dropped = true;
    }
}