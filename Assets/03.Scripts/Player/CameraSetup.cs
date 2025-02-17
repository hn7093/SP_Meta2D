using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 자신을 타겟 설정
        FollowCamera followCamera = FindObjectOfType<FollowCamera>();
        followCamera.target = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
