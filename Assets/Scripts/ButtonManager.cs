using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour

    
{
    public GameObject PausePanel;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    void Update()
    {
    }
    public void Pause()
    {
        Debug.Log("Pause");
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
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

    }
    public void Quit()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        Application.Quit();
    }
}
