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
    public Text level_text;
    private int _level;
    public int level
    {
        get => _level;
        set => _level = value;
    }

    // Start is called before the first frame update
    void Start()
    {

        _lives = 5;
        _score = 0;
        _level = 1;

        score_text.text = $"Score: {_score}";
        lives_text.text = $"Lives: {_lives}";
        level_text.text = $"Level: {_level}";
    }

    public void updateScore(int value)
    {
        _score += value;
        score_text.text = $"Score: {_score}";
    }

    public void decreaseLives()
    {
        _lives--;
        lives_text.text = $"Lives: {_lives}";
    }

    public void increaseLevel()
    {
        _level++;
        level_text.text = $"Level: {_level}";
    }
}
