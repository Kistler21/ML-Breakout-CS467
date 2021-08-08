using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    public Text score_text;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = PlayerPrefs.GetInt("score");
        print(_score);
        score_text.text = $"Score: {_score}";
    }
}
