using TMPro;
using UnityEngine;

public enum Difficulty
{
    easy,
    medium,
    hard,
}

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int highScore;
    public Difficulty curDifficulty;

    private const string SAVE_KEY = "HighScore";

    [Header("Difficulty TreshHolds")]
    [SerializeField] private int mediumTreshHold;
    [SerializeField] private int hardTreshHold;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI VerticalScore;
    [SerializeField] private TextMeshProUGUI HorizontalScore;

    public static GameManager Instance;
    

    void Start()
    {
        curDifficulty = Difficulty.easy;
        score = 0;
        VerticalScore.text = score.ToString();
        HorizontalScore.text = score.ToString();

        for (int i = 0; i < 5; i++)
        {
            LevelSpawner.Instance.SpawnLevel(curDifficulty);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            highScore = PlayerPrefs.GetInt(SAVE_KEY);
            return;
        }
        highScore = 0;

    }

    public void IncreaseScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(SAVE_KEY, score);
            PlayerPrefs.Save();
        }
        UpdateDifficulty();

        VerticalScore.text = score.ToString();
        HorizontalScore.text = score.ToString();

        LevelSpawner.Instance.SpawnLevel(curDifficulty);
    }
    private void UpdateDifficulty()
    {
        if(score > mediumTreshHold)
            curDifficulty = Difficulty.medium;

        if (score < hardTreshHold)
            curDifficulty = Difficulty.hard;
    }
        
}
