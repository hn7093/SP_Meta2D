using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI ScoreTMP;
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
        bestScore = PlayerPrefs.GetInt("BestPlane",0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore(int point)
    {
        currentScore += point;
        if(currentScore >= bestScore)
        {
            PlayerPrefs.SetInt("BestPlane",currentScore);
        }
        ScoreTMP.text = currentScore.ToString();
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
            int bestScore = PlayerPrefs.GetInt("BestPlane", 0);
            ResultBestScoreTMP.text = bestScore.ToString();
        }
    }
}
