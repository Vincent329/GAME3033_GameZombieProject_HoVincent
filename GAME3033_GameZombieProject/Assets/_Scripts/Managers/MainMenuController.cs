using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject creditsOverlay;
    bool creditsIsActive;
    private void Start()
    {
        creditsIsActive = false;
        creditsOverlay.SetActive(creditsIsActive);
        creditsButton.onClick.AddListener(ToggleCredits);
    }

    private void ToggleCredits()
    {
        creditsIsActive = !creditsIsActive;
        creditsOverlay.SetActive(creditsIsActive);
    }

}
