using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject rotatePoint;
    [SerializeField]
    private float _speedRotation = 20;
    
    private void FixedUpdate()
    {
        if (rotatePoint != null)
        {
            transform.RotateAround(rotatePoint.transform.position, Vector3.forward, Time.deltaTime * _speedRotation);
        }

    }
}
