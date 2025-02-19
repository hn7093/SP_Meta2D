using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int maxLife = 3;
    public int life = 3;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            life--;
            if (life <= 0)
            {
                // 점수 증가
                BlockScoreManager.Instance.AddScore(1);
                BlockGameManager.Instance.RemoveBlock(this);
                Destroy(gameObject);
            }
            else
            {
                // 투명도 감소
                Color color = _spriteRenderer.color;
                color.a = (float)life/maxLife;
                _spriteRenderer.color = color;
            }

        }
    }
    public void SetHp(int maxHp)
    {
        maxLife = maxHp;
        life = maxLife;

        // 생명 2부터는 색깔
        if(maxHp > 1)
            _spriteRenderer.color = GetRandomColor();
    }
    Color GetRandomColor()
    {
        float r = Random.Range(100, 250) / 255.0f;
        float g = Random.Range(100, 250) / 255.0f;
        float b = Random.Range(100, 250) / 255.0f;
        return new Color(r, g, b);
    }
}
