using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour

    
{
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        if (PausePanel != null)
        {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PausePanel.SetActive(true);
        }

    }

    public void PlayGame()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Pause()
    {
        Debug.Log("Pause");
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SettingsPanel.SetActive(false);
        AudioManagerNew.Instance.PlaySFX("Button");
    }

    public void Continue()
    {
        Debug.Log("Continue");
        AudioManagerNew.Instance.PlaySFX("Button");
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void MainMenu()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        PausePanel.SetActive(false);
        SceneManager.LoadScene("Level0");

    }
    public void Settings()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void SettingsMain()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        SettingsPanel.SetActive(true);
    }

    public void BackMain()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        Time.timeScale = 1f;
        SettingsPanel.SetActive(false);
        isPaused = false;
    }
    public void Quit()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        Debug.Log("Quit");
        Application.Quit();
        
    }
}
