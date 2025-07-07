using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer; 
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Soundcontrol : Singleton<Soundcontrol>
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Sprite ON;
    [SerializeField] private Sprite OFF;
    [SerializeField] private Button musicbutton;
    [SerializeField] private Button soundbutton;
    [SerializeField] private Image musicIcon;
    [SerializeField] private Image soundIcon;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button resetButton;


    void Start()
    {
        musicbutton.onClick.AddListener(ToggleMusic);
        soundbutton.onClick.AddListener(ToggleSound);


    }
    private void Awake()
    {
        GameMN.Instance.AddButtonHoverEffect(musicbutton);
        GameMN.Instance.AddButtonHoverEffect(soundbutton);
        GameMN.Instance.AddButtonHoverEffect(resetButton);
    }
    private void ToggleMusic()
    {
        if (music != null)
        {
            music.mute = !music.mute;
            UpdateButtonIcons();
        }
    }
    private void ToggleSound()
    {
        SoundManager.Instance.SetVolume(SoundManager.Instance.sfxSource.volume == 0 ? 1 : 0);
        UpdateButtonIcons();
    }
    private void UpdateButtonIcons()
    {
        if (music != null)
        {
            musicIcon.sprite = music.mute ? OFF : ON;

        }
        if (SoundManager.Instance.sfxSource.volume == 0)
        {
            soundIcon.sprite = OFF;
        }
        else
        {
            soundIcon.sprite = ON;
        }

    }
    public void VolumeControl()
    {
        if (music != null)
        {
            music.volume = soundSlider.value;
        }
    }
    void Update()
    {
        VolumeControl();
    }
    public void ResetGame()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}

