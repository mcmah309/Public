using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); works
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
    public void HighScores()
    {
        SceneManager.LoadScene("HighScoreTable");
    }
    public void Store()
    {
        SceneManager.LoadScene("Store");
    }
    public void Quit()
    {
        //PlayerPrefs.SetInt("NumberOfCoins", 10000000);
        Application.Quit();
    }
}
