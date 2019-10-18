using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
public class EndScoreManager : MonoBehaviour
{

    private List<Transform> highscoreEntryTransformList;

    public TMP_InputField Name;
    public Text Nameholder;
    public TMP_Text score;
    public GameObject RunningScore;
    public Text littlescore;
    private int intscore;

    private void Awake()
    {
       // PlayerPrefs.DeleteAll();
        /*PlayerPrefs.DeleteAll();
        AddHighscoreEntry(1000000, "CMK");
        AddHighscoreEntry(897621, "JOE");
        AddHighscoreEntry(872931, "DAV");
        AddHighscoreEntry(785123, "CAT");
        AddHighscoreEntry(542024, "MAX");
        AddHighscoreEntry(68245, "AAA");
        AddHighscoreEntry(785123, "CAT");
        AddHighscoreEntry(20, "jab");*/

    }
    void OnEnable()
    {
        score.text = RunningScore.GetComponent<Score>().score.ToString();
        littlescore.text = score.text;
        intscore = Int32.Parse(score.text);
        //Debug.Log(intscore);
        int coins = intscore / 5;
        //coins += 100000;
        int NumberOfCoins = PlayerPrefs.GetInt("NumberOfCoins");
        NumberOfCoins += coins;
        PlayerPrefs.SetInt("NumberOfCoins", NumberOfCoins);
        CheckForHighScore(intscore);
    }
    public void SubmitName()
    {
        if(Name.text.ToLower().IndexOf(' ') == -1 && Name.text.Length <13 && Name.text != "")
        {
            Nameholder.text = Name.text;
            GameObject.Find("NameInput").SetActive(false);
            GameObject.Find("SubmitScore").SetActive(false);
            SubmitScore(intscore, Name.text);
        }
    }
    void CheckForHighScore(int score)
    {

        //PlayerPrefs.DeleteAll();
        string jsonString = PlayerPrefs.GetString("highscoreTable");//gets the high scores in json format
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);//turns the json data into highscores, which is a list of highscore entries
        bool newscore = true;
        //Debug.Log(jsonString);
        if (highscores == null )
        {
            GameObject.Find("posText").GetComponent<Text>().color = Color.green;
            GameObject.Find("scoreText").GetComponent<Text>().color = Color.green;
            GameObject.Find("nameText").GetComponent<Text>().color = Color.green;
            GameObject.Find("posText").GetComponent<Text>().text = "1st";
            GameObject.Find("scoreText").GetComponent<Text>().text = score.ToString();
            GameObject.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
            return;
        }

        else
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                //Debug.Log(score + "score");
                //Debug.Log(highscores.highscoreEntryList[i].score + "list");
                if (score > highscores.highscoreEntryList[i].score || highscores.highscoreEntryList.Count < 10)
                {
                    //AddHighscoreEntry(score, Nameholder.text);
                    newscore = false;
                    break;

                }
            }
        }
        if (newscore)
        {
            GameObject.Find("highscoreEntryContainer").SetActive(false);
            GameObject.Find("SubmitScore").SetActive(false);
            GameObject.Find("NameInput").SetActive(false);
            GameObject.Find("PosText").SetActive(false);
            GameObject.Find("ScoreText").SetActive(false);
            GameObject.Find("NameText").SetActive(false);
            return;
        }
        jsonString = PlayerPrefs.GetString("highscoreTable");//gets the high scores in json format
        highscores = JsonUtility.FromJson<Highscores>(jsonString);//turns the json data into highscores, which is a list of highscore entries

        // Sort entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }
        int scoreplace = 10;
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            //Debug.Log(highscores.highscoreEntryList[i].score);
            if (score > highscores.highscoreEntryList[i].score)
            {
                scoreplace = i+1;
                break;
            }
            else
            {
                scoreplace = highscores.highscoreEntryList.Count + 1;
            }
        }
        string rankString;
        switch (scoreplace)
        {
            default:
                rankString = scoreplace + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        GameObject.Find("posText").GetComponent<Text>().text = rankString;
        // Highlight if First
        if (scoreplace == 1)
        {
            GameObject.Find("posText").GetComponent<Text>().color = Color.green;
            GameObject.Find("scoreText").GetComponent<Text>().color = Color.green;
            GameObject.Find("nameText").GetComponent<Text>().color = Color.green;
        }
        // Set tropy
        switch (scoreplace)
        {
            default:
                GameObject.Find("trophy").gameObject.SetActive(false);
                break;
            case 1:
                GameObject.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("FFD200");
                break;
            case 2:
                GameObject.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("C6C6C6");
                break;
            case 3:
                GameObject.Find("trophy").GetComponent<Image>().color = UtilsClass.GetColorFromString("B76F56");
                break;

        }
        GameObject.Find("posText").GetComponent<Text>().text = rankString;
    }

    private void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable] //Unity can acccess the elements from outside the class in a unity readable way
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
    void SubmitScore(int score, string name)
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");//gets the high scores in json format
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);//turns the json data into highscores, which is a list of highscore entries
        if(highscores == null)
        {
            AddHighscoreEntry(score, name);
        }
        else if(highscores.highscoreEntryList.Count >= 10)
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        // Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }
            /*for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                Debug.Log(highscores.highscoreEntryList[i].score);
            }*/
            highscores.highscoreEntryList.RemoveAt(9);
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();

            //Debug.Log(jsonString);
            //Debug.Log("its at 10 ");
            AddHighscoreEntry(score, name);

            //jsonString = PlayerPrefs.GetString("highscoreTable");
            //Debug.Log(jsonString);
        }
        else
        {
            AddHighscoreEntry(score, name);
        }
        jsonString = PlayerPrefs.GetString("highscoreTable");
        Debug.Log(jsonString);
    }
}

