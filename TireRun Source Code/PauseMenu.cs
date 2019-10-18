using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //Advertisement.Banner.Hide();
        //FindObjectOfType<AdManager>().showBanner = false;
        SceneManager.LoadScene("MainMenu");
    }
}
