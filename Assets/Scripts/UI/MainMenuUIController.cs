using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            SceneLoaderManager.Instance.LoadExploreGameplay();
        });

        _exitButton.onClick.AddListener(() =>
         {
             Application.Quit();
         });

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
