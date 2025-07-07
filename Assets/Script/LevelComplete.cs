using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelCompleteUI : Singleton<LevelCompleteUI>
{
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private GameObject GameCompletePanel;

    [SerializeField] private Text TotalScoreText;
    private void Start()
    {
        levelCompletePanel.SetActive(false);

        GameMN.Instance.AddButtonHoverEffect(backToMenuButton);
        GameMN.Instance.AddButtonHoverEffect(nextLevelButton);
        nextLevelButton.onClick.AddListener(NextLevel);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void Show(int finalScore)
    {
        StartCoroutine(DelayShowPanel());

        IEnumerator DelayShowPanel()
        {
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 0f;
            levelCompletePanel.SetActive(true);
            scoreText.text = ""+finalScore;
            ScoreMN.Instance.SaveScore();
            ChoseLevel.Instance.UnlockNextLevel(SceneManager.GetActiveScene().buildIndex);
            yield break;
        }
        return;
    }
    private void NextLevel()
    {
        Time.timeScale = 1f;
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            int totalScore = ScoreMN.Instance.GetTotalScore();
            TotalScoreText.text = "" + totalScore;
            GameMN.Instance.Transittion(GameCompletePanel, levelCompletePanel);
            StartCoroutine(DelayPause());

            IEnumerator DelayPause()
            {
                yield return new WaitForSecondsRealtime(1f);
                Time.timeScale = 0f;
            }
        }
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Đổi tên scene nếu khác
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int finalScore = ScoreMN.Instance.GetScore();
            ChoseLevel.Instance.UnlockNextLevel(SceneManager.GetActiveScene().buildIndex);
            SoundManager.Instance.Play("End");
            Show(finalScore);
        }
    }
}
