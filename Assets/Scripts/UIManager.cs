using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool Portrait;
    private static int Width;
    public static UIManager Instance;

    [SerializeField] private GameObject VerticalUI;
    [SerializeField] private GameObject HorizontalUI;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject VerticalGameOverScreen;
    [SerializeField] private TextMeshProUGUI VerticalHighScoreText;
    [SerializeField] private TextMeshProUGUI VerticalScoreText;
    [SerializeField] private GameObject HorizontalGameOverScreen;
    [SerializeField] private TextMeshProUGUI HorizontalHighScoreText;
    [SerializeField] private TextMeshProUGUI HorizontalScoreText;

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

    void Start()
    {
        Width = Screen.width;

        if (Screen.height >= Width) { Portrait = true; }
        else { Portrait = false; }
    }

    void Update()
    {
        if (Width != Screen.width)
        {
            Width = Screen.width;

            if (Screen.height >= Width) { Portrait = true; }
            else { Portrait = false; }
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (Portrait) {
            HorizontalUI.gameObject.SetActive(false);
            VerticalUI.gameObject.SetActive(true);
        }
        else
        {
            HorizontalUI.gameObject.SetActive(true);
            VerticalUI.gameObject.SetActive(false);
        }
    }

    public void EnableGameOverScreen()
    {
        HorizontalGameOverScreen.SetActive(true);
        HorizontalHighScoreText.text = $"High Score: {GameManager.Instance.highScore}";
        HorizontalScoreText.text = $"Score: {GameManager.Instance.score}";
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(1).buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).buildIndex);
    }


}
