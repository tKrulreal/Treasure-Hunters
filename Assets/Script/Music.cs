using UnityEngine;

public class Music : Singleton<Music>
{
    [SerializeField] private AudioSource musicSource;
    void Start()
    {
        DontDestroyOnLoad(this);
        if (musicSource != null&& !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
    
}
