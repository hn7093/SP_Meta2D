using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // 따라갈 타겟
    public float smoothSpeed = 5f;
    public Vector3 offset;  // 타겟과의 거리
    public bool limitX;
    public float minX;
    public float maxX;
    public bool limitY;

    public float minY;
    public float maxY;
    void LateUpdate()
    {
        if (target == null) return;

        // 부드러운 카메라 이동
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;
        if (limitY)
        {
            desiredPosition.y = Math.Clamp(desiredPosition.y, minY, maxY);
        }
        if (limitX)
        {
            desiredPosition.x = Math.Clamp(desiredPosition.x, minX, maxX);
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
