using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    //Declares text object that the life counter will be bound to in Unity
    public Text lives_text;
    private int _lives;
    public int lives
    {
        get => _lives;
        set => _lives = value;
    }
    public Text score_text;
    private int _score;
    public int score
    {
        get => _score;
        set => _score = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        _lives = 5;
        _score = 0;

        score_text.text = $"SCORE: {_score}";
        lives_text.text = $"LIVES: {_lives}";
    }

    public void updateScore(int value)
    {
        _score += value;
        score_text.text = $"SCORE: {_score}";
    }

    public void decreaseLives()
    {
        _lives--;
        lives_text.text = $"LIVES: {_lives}";
    }
}
