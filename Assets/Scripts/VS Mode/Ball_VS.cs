using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4

public class Ball_VS : MonoBehaviour
{
    // variable for physics
    Rigidbody2D rb;

    // variable for if ball is in play
    public bool in_play;

    // variable for paddle
    public Transform paddle;

    public float speed;

    public GM gm;

    public GM computerGM;

    public bool isInputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        // initializing variable
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int RandNum = Random.Range(0, 2);
        // conditional to check if ball is in play
        if (!in_play)
        {
            transform.position = paddle.position;
        }

        // conditional to use spacebar to start game 
        if (Input.GetButtonDown("Jump") && !in_play && isInputEnabled)
        {
            in_play = true;

            // force
            if (RandNum == 0)
            {
                rb.AddForce((Vector2.up + Vector2.left) * speed / 2);
            }
            else
            {
                rb.AddForce((Vector2.up + Vector2.right) * speed / 2);
            }
        }
    }

    // function for bottom screen edge when ball misses paddle 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))
        {
            Debug.Log("Ball hit the bottom of the screen");
            rb.velocity = Vector2.zero;
            in_play = false;

            //Decrements life counter variable in GM script
            gm.decreaseLives();

            // Check if game over
            if (gm.lives == 0 && computerGM.lives == 0)
            {
                GameOver();
            }
            else if (gm.lives == 0)
            {
                isInputEnabled = false;
            }
        }
    }

    public void GameOver()
    {
        if (gm.score > computerGM.score)
        {
            PlayerPrefs.SetString("winner", "player");
        }
        else if (gm.score < computerGM.score)
        {
            PlayerPrefs.SetString("winner", "computer");
        }
        else
        {
            PlayerPrefs.SetString("winner", "tie");
        }
        SceneManager.LoadScene("VS Game Over");
    }

    // Function for bricks/destroyed bricks
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("yellowBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(1);
        }
        else if (collision.transform.CompareTag("greenBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(3);
        }
        else if (collision.transform.CompareTag("orangeBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(5);
        }
        else if (collision.transform.CompareTag("redBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(7);
        }
    }
}
