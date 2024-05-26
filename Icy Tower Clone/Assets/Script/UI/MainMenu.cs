using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private Transform settingPanel;

    public void PlayButton()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingButton()
    {
        settingPanel.gameObject.SetActive(true);
    }
}
