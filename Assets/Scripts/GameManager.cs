using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int highScore;
    public Difficulty curDifficulty;

    private const string SAVE_KEY = "HighScore";

    [Header("Difficulty TreshHolds")]
    [SerializeField] private int mediumTreshHold;
    [SerializeField] private int hardTreshHold;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI VerticalScore;
    [SerializeField] private TextMeshProUGUI HorizontalScore;
    public enum Difficulty
    {
        easy,
        medium,
        hard,
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
    }
    private void UpdateDifficulty()
    {
        if(score > mediumTreshHold)
            curDifficulty = Difficulty.medium;

        if (score < hardTreshHold)
            curDifficulty = Difficulty.hard;
    }
        
}
