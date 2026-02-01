using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool Portrait;
    private static int Width;

    [SerializeField] private GameObject VerticalUI;
    [SerializeField] private GameObject HorizontalUI;

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

}
