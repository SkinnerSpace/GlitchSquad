using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public SoundSource[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public AudioSource ambient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        /*if (ambient != null)
        {
            ambient.Play();
        }*/
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        SoundSource s = Array.Find(musicSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name, float volume = 1f)
    {
        SoundSource s = Array.Find(sfxSounds, x => x.Name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
        }
        else
        {
            sfxSource.volume = volume;

            sfxSource.PlayOneShot(s.clip);
        }
    }
}
