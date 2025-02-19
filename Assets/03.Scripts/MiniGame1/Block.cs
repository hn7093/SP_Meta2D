using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 점수 증가
            BlockScoreManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
