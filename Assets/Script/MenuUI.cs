using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MenuUI : Singleton<MenuUI>
{
    [SerializeField] private Button startButton;
    [SerializeField] private  Button exitButton;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject choselevel;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        GameMN.Instance.AddButtonHoverEffect(startButton);
        GameMN.Instance.AddButtonHoverEffect(exitButton);
    }

    void OnStartButtonClicked()
    {
        GameMN.Instance.Transittion(choselevel, menuPanel);
    }
    void OnExitButtonClicked()
    {
        Application.Quit();
    }
    

}