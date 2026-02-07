using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool Portrait;
    private static int Width;
    public static UIManager Instance;
    private bool isPaused = false;
    private bool InputUIEnabled = false;

    private const string SAVE_KEY = "InputUI";

    [SerializeField] private GameObject VerticalUI;
    [SerializeField] private GameObject HorizontalUI;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject VerticalGameOverScreen;
    [SerializeField] private TextMeshProUGUI VerticalHighScoreText;
    [SerializeField] private TextMeshProUGUI VerticalScoreText;

    [SerializeField] private GameObject HorizontalGameOverScreen;
    [SerializeField] private TextMeshProUGUI HorizontalHighScoreText;
    [SerializeField] private TextMeshProUGUI HorizontalScoreText;

    [Header("PauseMenu")]
    [SerializeField] private GameObject VerticalPauseMenu;
    [SerializeField] private GameObject VerticalPauseBtn;
    [SerializeField] private GameObject VerticalExitBtn;

    [SerializeField] private GameObject HorizontalPauseMenu;
    [SerializeField] private GameObject HorizontalPauseBtn;
    [SerializeField] private GameObject HorizontalExitBtn;

    [Header("InputUI")]
    [SerializeField] private GameObject VerticalInputUI;
    [SerializeField] private GameObject HorizontalInputUI;

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
            var value = PlayerPrefs.GetString(SAVE_KEY);
            InputUIEnabled = value == "True";
        }
        else
        {
            InputUIEnabled = true;
        }      

        ToggleInput();
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
        SceneManager.LoadScene(0);
    }

    public void PauseGame(bool enable)
    {
        
        if (enable)
        {
            Time.timeScale = 0;
            isPaused = true;
            VerticalPauseMenu.SetActive(true);
            HorizontalPauseMenu.SetActive(true);
            VerticalPauseBtn.SetActive(false);
            VerticalExitBtn.SetActive(true);
            HorizontalPauseBtn.SetActive(false);
            HorizontalExitBtn.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = true;
            VerticalPauseMenu.SetActive(false);
            HorizontalPauseMenu.SetActive(false);
            VerticalPauseBtn.SetActive(true);
            VerticalExitBtn.SetActive(false);
            HorizontalPauseBtn.SetActive(true);
            HorizontalExitBtn.SetActive(false);
        }
    }



    public void UpdateInputUI()
    {
        InputUIEnabled = !InputUIEnabled;

        PlayerPrefs.SetString(SAVE_KEY, InputUIEnabled.ToString());
        PlayerPrefs.Save();

        ToggleInput();
    }

    private void ToggleInput()
    {
        if (InputUIEnabled)
        {
            VerticalInputUI.SetActive(true);
            HorizontalInputUI.SetActive(true);
        }
        else
        {
            VerticalInputUI.SetActive(false);
            HorizontalInputUI.SetActive(false);
        }
    }

}
