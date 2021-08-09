using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    public Text score_text;
    public bool isVSMode;

    // Start is called before the first frame update
    void Start()
    {
        if (!isVSMode)
        {
            int score = PlayerPrefs.GetInt("score");
            score_text.text = $"Score: {score}";
        }
        else
        {
            string winner = PlayerPrefs.GetString("winner");
            if (winner == "player")
            {
                score_text.text = "You won!";
            }
            else if (winner == "computer")
            {
                score_text.text = "You lost!";
            }
            else if (winner == "tie")
            {
                score_text.text = "You tied!";
            }
        }
    }
}
