using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject quitButton;


    void Start()
    {
    #if UNITY_WEBGL
        quitButton.SetActive(false);
    #endif
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

}
