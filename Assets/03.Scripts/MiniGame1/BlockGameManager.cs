using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BlockGameManager : MonoBehaviour
{
    static BlockGameManager instance;
    public static BlockGameManager Instance { get { return instance; } }
    [SerializeField] private GameObject ballPrefab;
    public GameState gameState = GameState.Ready;
    public static bool isFirstLoading = true;
    private PlayerBlock player;
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
        player = FindObjectOfType<PlayerBlock>();
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }
    public void StartGame()
    {
        ChangeState(GameState.Playing);
        BlockScoreManager.Instance.ActiveUI(gameState);
        if(player == null)
        {
            Debug.Log("player null");
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
