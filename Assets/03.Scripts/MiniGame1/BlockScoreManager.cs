using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlockScoreManager : MonoBehaviour
{
    static BlockScoreManager instance;
    public static BlockScoreManager Instance { get { return instance; } }

    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI bestTMP;
    [SerializeField] GameObject reayUI;
    [SerializeField] GameObject resultUI; 
    [SerializeField] TextMeshProUGUI ResultScoreTMP;
    [SerializeField] TextMeshProUGUI ResultBestScoreTMP;
    private int currentScore = 0;
    private int bestScore = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestBlock",0);
    }
    public void AddScore(int point)
    {
        currentScore += point;
        if(currentScore >= bestScore)
        {
            PlayerPrefs.SetInt("BestBlock",currentScore);
        }
        scoreTMP.text = currentScore.ToString();
        ResultScoreTMP.text = currentScore.ToString();
    }
    public void ActiveUI(GameState gameState)
    {
        if (gameState == GameState.Ready)
        {
            reayUI.SetActive(true);
            resultUI.SetActive(false);
        }
        else if (gameState == GameState.Playing)
        {
            reayUI.SetActive(false);
            resultUI.SetActive(false);
        }
        else if (gameState == GameState.Stop)
        {
            reayUI.SetActive(false);
            resultUI.SetActive(true);
            int bestScore = PlayerPrefs.GetInt("BestBlock", 0);
            ResultBestScoreTMP.text = bestScore.ToString();
        }
    }
}
