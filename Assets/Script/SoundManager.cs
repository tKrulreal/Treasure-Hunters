using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Sound[] sounds;

    private Dictionary<string, AudioClip> soundDict;
    public AudioSource sfxSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        soundDict = new Dictionary<string, AudioClip>();
        foreach (Sound s in sounds)
        {
            if (!soundDict.ContainsKey(s.name))
                soundDict.Add(s.name, s.clip);
        }
    }

    public void Play(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            sfxSource.PlayOneShot(soundDict[name]);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void SetVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
