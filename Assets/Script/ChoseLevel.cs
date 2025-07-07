using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditorInternal; // Ensure you have DOTween installed
using UnityEngine.EventSystems;

public class ChoseLevel : Singleton<ChoseLevel>
{

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject choselevel;
    [SerializeField] private Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        addlistener();
    }
    void OnBackButtonClicked()
    {
        backButton.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        GameMN.Instance.Transittion(menuPanel, choselevel);


    }
    [SerializeField] private Button[] levelButtons;

    void OnEnable()
    {
        UpdateLevelButtons();
    }

    public void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = (i < unlockedLevel);
            levelButtons[i].interactable = isUnlocked;
            levelButtons[i].GetComponent<Image>().color = isUnlocked ? Color.white : Color.gray;
        }
    }

    // Gọi hàm này khi hoàn thành một cấp độ
    public void UnlockNextLevel(int completedLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (completedLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", completedLevel + 1);
            PlayerPrefs.Save();
            
            UpdateLevelButtons();
        }
    }
    public void OnLevelButtonClicked(int levelIndex)
    {
        if (levelButtons[levelIndex].interactable)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex + 1);
        }
    }
    public void addlistener()
{
    for (int i = 0; i < levelButtons.Length; i++)
    {
        int index = i;
        levelButtons[i].onClick.RemoveAllListeners(); // tránh gắn trùng
        levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(index));
    }
}


}
