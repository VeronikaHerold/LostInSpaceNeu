using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        Application.Quit(); 
    }
    public void GoToSettingsMenu()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        SceneManager.LoadScene("SettingsMenu");
    }
    public void GoToMainMenu()
    {
        AudioManagerNew.Instance.PlaySFX("Button");
        SceneManager.LoadScene("MainMenu");
    }

}
