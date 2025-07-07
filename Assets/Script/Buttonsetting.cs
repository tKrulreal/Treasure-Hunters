using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;


public class Buttonsetting :Singleton<Buttonsetting>
{
    [SerializeField] private GameObject settingsPanel;

    public void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(settingsPanel);
    }
    public void OpenSetting()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        Time.timeScale = settingsPanel.activeSelf ? 0f : 1f;

    }
}
