using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseExtraLife : MonoBehaviour
{
    public GameObject highscoreEntryContainer;
    public GameObject InputName;
    public GameObject SubmitScore;
    public GameObject PosText;
    public GameObject ScoreText;
    public GameObject NameText;
  public void UseLife()
    {
        //PlayerPrefs.SetInt("NumberOfLives", 100);
        //bool canUse = GameObject.Find("wheel").GetComponent<PlayerMovement>().canUseLife;
        bool canUse = GameObject.Find("wheel").GetComponent<PlayerMovementTouch>().canUseLife;
        if (canUse)
        {
            int lives = PlayerPrefs.GetInt("NumberOfLives");
            if (!(lives > 0))
            {
                return;
            }
            else
            {
                lives -= 1;
                PlayerPrefs.SetInt("NumberOfLives", lives);
                //GameObject.Find("wheel").GetComponent<PlayerMovement>().canUseLife = false;
                GameObject.Find("wheel").GetComponent<PlayerMovementTouch>().canUseLife = false;

                FindObjectOfType<AudioManager>().Volume("HittingCar", 1);
                FindObjectOfType<AudioManager>().Play("WarbleOfWheel");
                //GameObject.Find("wheel").GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find("wheel").GetComponent<PlayerMovementTouch>().enabled = true;
                GameObject.Find("wheel").GetComponent<PitchRollYaw>().enabled = true;
                GameObject.Find("wheel").GetComponent<PitchRollYaw>().maxangle = 0;
                GameObject.Find("wheel").GetComponentInChildren<RotateWheel>().enabled = true;
                GameObject.Find("score").GetComponent<Score>().on = true;
                //GameObject.Find("highscoreEntryContainer").SetActive(true); doesnt work for some reason when the game object is already disabled
                //GameObject.Find("InputName").SetActive(true);
                //GameObject.Find("SubmitScore").SetActive(true);
                highscoreEntryContainer.SetActive(true);
                InputName.SetActive(true);
                SubmitScore.SetActive(true);
                PosText.SetActive(true);
                NameText.SetActive(true);
                ScoreText.SetActive(true);

                GameObject.Find("EndGameScore").SetActive(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            return;
        }
    }

}
