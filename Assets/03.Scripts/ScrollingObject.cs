using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;
    public bool refeat = false;
    public int imageCount = 2; // 반복 되는 이미지 수
    private float width; // 스프라이트 가로 길이
    
    void Awake()
    {
        if (refeat)
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            width = boxCollider2D.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 실행 중일 떄만
        if(!(GameManager.Instance.gameState == GameState.Playing)) return;
        
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (refeat && transform.position.x <= -width * (imageCount/2.0f))
        {
            Reposition();
        }
    }
    private void Reposition()
    {
        // 가로 길이 x3만큼 이동하여 반복 배치
        Vector2 offset = new Vector2(width * imageCount, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
