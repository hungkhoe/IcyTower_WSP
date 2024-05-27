using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private Transform settingPanel;
    [SerializeField] private Transform menuPanel;
    [SerializeField] private Transform loadingPanel;

    [SerializeField] Slider loadingSprite;

    public void PlayButton()
    {
        StartCoroutine(LoadLevelAsync(playScene));
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void SettingButton()
    {
        settingPanel.gameObject.SetActive(true);
    }

    IEnumerator LoadLevelAsync(string leveltoLoad)
    {       
        menuPanel.gameObject.SetActive(false);
        loadingPanel.gameObject.SetActive(true);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(leveltoLoad);
        loadOperation.allowSceneActivation = false;

        float progressValue = 0;

        while (!loadOperation.isDone && progressValue < 1)
        {
            progressValue += 0.005f;
            loadingSprite.value = progressValue;
            yield return null;
        }

        loadOperation.allowSceneActivation = true;
    }
}
