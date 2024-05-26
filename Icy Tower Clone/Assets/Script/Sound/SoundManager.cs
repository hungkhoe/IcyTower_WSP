using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource music;
    public AudioSource sfx;

    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private AudioClip[] musicClips;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Init();
    }   

    private void Init()
    {
        AudioSource[] source = GetComponents<AudioSource>();
        music = source[0];
        sfx = source[1];
    }

    public void SetMuic(MusicClip _music)
    {
        music.clip = musicClips[(int)_music];
        music.Play();
    }
    public void SetSFX(SoundClip _sound)
    {
        sfx.clip = soundClips[(int)_sound];
        sfx.Play();
    }
}

public enum MusicClip
{
    Home,
    InGame
}

public enum SoundClip
{

}