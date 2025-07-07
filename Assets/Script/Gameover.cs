using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMenuButton;

    private void Start()
    {
        GameMN.Instance.AddButtonHoverEffect(restartButton);
        GameMN.Instance.AddButtonHoverEffect(backToMenuButton);
        restartButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);

    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene"); // Đổi tên scene nếu khác
    }

    
}
