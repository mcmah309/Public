using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public int x;
    public int score;
    public bool on = true;


    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            score =x + (int)Time.timeSinceLevelLoad;
        }
        scoreText.text = score.ToString("0");
    }
}
