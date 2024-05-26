using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        musicToggle.onValueChanged.AddListener(delegate {
            MusicToggle();
        });

        soundToggle.onValueChanged.AddListener(delegate {
            SoundToggle();
        });

        musicSlider.onValueChanged.AddListener(delegate {
            MusicSlider();
        });

        soundSlider.onValueChanged.AddListener(delegate {
            SFXSlider();
        });
    }

    public void CloseSettingButton()
    {
        gameObject.SetActive(false);
    }

    private void MusicToggle()
    {
        SoundManager.Instance.music.mute = !musicToggle.isOn;
    }
    private void SoundToggle()
    {
        SoundManager.Instance.sfx.mute = !soundToggle.isOn;
    }

    private void MusicSlider()
    {
        SoundManager.Instance.music.volume = musicSlider.value;
    }
    private void SFXSlider()
    {
        SoundManager.Instance.music.volume = musicSlider.value;
    }
}
