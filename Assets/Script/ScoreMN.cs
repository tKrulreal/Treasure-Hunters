using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMN : Singleton<ScoreMN>
{
    private int currentScore = 0;

    [SerializeField] private Text scoreText;


    private void Start()
    {
        DontDestroyOnLoad(this);

        currentScore = 0;
        UpdateUI();

        Observer.Addlistener("OnAddScore", OnAddScore);
    }

    private void OnAddScore(object[] data)
    {
        if (data.Length > 0 && data[0] is int)
        {
            AddScore((int)data[0]);
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void SaveScore()
    {
        int levelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Score_Level_" + levelIndex, currentScore);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        Observer.Removelistener("OnAddScore", OnAddScore);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.buildIndex >= 1)
        {
            currentScore = 0;
            UpdateUI();
        }
    }
    public int GetTotalScore()
    {
    int total = 0;
    int totalLevels = SceneManager.sceneCountInBuildSettings - 1; 

    for (int i = 1; i <= totalLevels; i++)
    {
        total += PlayerPrefs.GetInt("Score_Level_" + i, 0);
    }

    return total;
    }


}
