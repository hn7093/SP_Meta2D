using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;
public class BGPool : MonoBehaviour
{
    // 발사체
    [SerializeField] private GameObject obstaclePrefab;
    // 발사 위치
    public Transform spawnTransform;

    // 발사 간격
    public float spawnRate = 0.8f;

    // 마지막 발사 시점
    private float lastSpawnTime;
    private IObjectPool<Obstacle> obstaclePool;
    private List<Obstacle> activeObstacles = new List<Obstacle>();
    void Awake()
    {
        obstaclePool = new ObjectPool<Obstacle>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroryBullet, maxSize: 20);
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        // 실행 중일 떄만
        if(!(GameManager.Instance.gameState == GameState.Playing)) return;
        // 발사 간격 체크
        if (Time.time >= lastSpawnTime + spawnRate)
        {
            // 시간 갱신
            lastSpawnTime = Time.time;
            SpawnObstacle();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 기둥 충돌시 풀에 반환
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacle.Release();
        }
    }
    // 생성
    private Obstacle CreateBullet()
    {
        Obstacle obstacle = Instantiate(obstaclePrefab).GetComponent<Obstacle>();
        obstacle.SetObstaclePool(obstaclePool);
        return obstacle;
    }
    // 가져오기
    private void OnGetBullet(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(true);
        activeObstacles.Add(obstacle);
    }
    // 반환
    private void OnReleaseBullet(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
        activeObstacles.Remove(obstacle);
    }
    // 최대 갯수 초과 시 삭제
    private void OnDestroryBullet(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
    }
    public void SpawnObstacle()
    {
        Obstacle obstacle = obstaclePool.Get();
        obstacle.SetRandomPlace(spawnTransform.position);
    }
    
    public void Reset()
    {
        // 모두 Release
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            obstaclePool.Release(activeObstacles[i]);
        }
        activeObstacles.Clear();
    }
}
