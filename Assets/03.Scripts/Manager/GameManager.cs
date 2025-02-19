using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    Ready,
    Playing,
    Stop
}
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameState gameState = GameState.Ready;
    public static bool isFirstLoading = true;
    private JumpBox player;
    private BGPool bGPool;
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
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
            player = FindObjectOfType<JumpBox>();
            bGPool = FindObjectOfType<BGPool>();
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
        UIManager.Instance.ActiveUI(gameState);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameStop()
    {
        ChangeState(GameState.Stop);
        UIManager.Instance.ActiveUI(gameState);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
