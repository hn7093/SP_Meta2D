using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // 따라갈 타겟
    public float smoothSpeed = 3f;
    public Vector3 offset;  // 타겟과의 거리
    public bool limitX;
    public float minX;
    public float maxX;
    public bool limitY;

    public float minY;
    public float maxY;
    void FixedUpdate()
    {
        if (target == null) return;

        // 부드러운 카메라 이동
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;
        if (limitY)
        {
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        }
        if (limitX)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
