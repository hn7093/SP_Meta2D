using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Obstacle : MonoBehaviour
{

    public float highPosY = 1;
    public float lowPosY = -1;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f; // 배치 간격
    private IObjectPool<Obstacle> obstaclePool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRandomPlace(Vector3 lastPosition)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        // top과 bottom은 이 오브젝트의 자식이기에 localPosition 변경
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        // 이 오브젝트(기둥) 위치 설정
        Vector3 placePosition = lastPosition;
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어이고 죽기 않은 경우에만
        JumpBox player = collision.GetComponent<JumpBox>();
        if(player && !player.isDead)
        {
            Debug.Log("OnTriggerExit2D : " + collision.gameObject.name);
            UIManager.Instance.AddScore(1);
        }
    }
    public void SetObstaclePool(IObjectPool<Obstacle> obstaclePool)
    {
        this.obstaclePool = obstaclePool;
    }
    public void Release()
    {
        obstaclePool.Release(this);
    }
}
