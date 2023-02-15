using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    private Transform target;
    private float travelSpeed = 10f;
    private float turnSpeed = 1f;

    public void Setup(Transform target, float travelSpeed, float turnSpeed)
    {
        this.target = target;
        this.travelSpeed = travelSpeed;
        this.turnSpeed = turnSpeed;
    }

    private void Update()
    {
        
    }
}
