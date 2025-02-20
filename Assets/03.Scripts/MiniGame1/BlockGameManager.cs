using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BlockGameManager : MonoBehaviour
{
    static BlockGameManager instance;
    public static BlockGameManager Instance { get { return instance; } }
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject blockPrefab;
    public GameState gameState = GameState.Ready;
    public static bool isFirstLoading = true;
    private int blockCount = 20;
    private List<Block> blocks;
    private PlayerBlock player;
    private int level = 1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ChangeState(GameState.Ready);
    }
    // Start is called before the first frame update
    void Start()
    {
        blocks = new List<Block>(blockCount);
        player = FindObjectOfType<PlayerBlock>();
        if (!isFirstLoading)
        {
            StartGame();
            SpawnBlock();
        }
        else
        {
            isFirstLoading = false;
            SpawnBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnBlock()
    {
        
        for (int i = 0; i < blockCount; i++)
        {
            float x = (i % 5) * 1.6f - 3.2f;
            float y = (i / 5) * 1.0f;

            Block block = Instantiate(blockPrefab).GetComponent<Block>();
            block.transform.position = new Vector3(x, y, 0);
            blocks.Add(block);
            // 레벨별로 최대 HP를 랜덤 설정
            int hp = Random.Range(1, level+1);
            blocks[i].SetHp(hp);
        }
    }
    public void RemoveBlock(Block block)
    {
        blocks.Remove(block);
        // 남은게 없으면 다음 레벨로 생성
        if(blocks.Count == 0)
        {
            level++;
            SpawnBlock();
        }
    }
    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }
    public void StartGame()
    {
        ChangeState(GameState.Playing);
        BlockScoreManager.Instance.ActiveUI(gameState);
        if (player == null)
        {
            player = FindObjectOfType<PlayerBlock>();
        }
        // 시작 볼 생성
        Instantiate(ballPrefab, player.transform.position + Vector3.up, Quaternion.identity);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameStop()
    {
        ChangeState(GameState.Stop);
        BlockScoreManager.Instance.ActiveUI(gameState);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
