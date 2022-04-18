using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndMenuController : MonoBehaviour
{
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MenuButton;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Start()
    {
        AppEvents.InvokeOnMouseCursorEnable(true);
        RestartButton.onClick.AddListener(RestartGame);
        MenuButton.onClick.AddListener(LoadMenu);

        finalScoreText.text = "Final Score: " + StaticData.score;
    }

    private void RestartGame()
    {
        SceneLoadManager.Instance.LoadGame();
    }

    private void LoadMenu()
    {
        SceneLoadManager.Instance.LoadMenuScreen();
    }

}
