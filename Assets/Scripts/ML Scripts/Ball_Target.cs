using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4

public class Ball_Target : MonoBehaviour
{
    // variable for physics
    Rigidbody2D rb;

    // variable for if ball is in play
    public bool in_play;

    // variable for paddle
    public Transform paddle;

    public float speed;
    public Paddle_Agent paddleAgent;
    public GM gm;

    // Start is called before the first frame update
    void Start()
    {
        // initializing variable
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // conditional to check if ball is in play
        if (!in_play)
        {
            transform.position = paddle.position;
        }
    }

    public void playBall()
    {
        int RandNum = Random.Range(0, 2);
        if (!in_play)
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
            paddleAgent.ballMissed();

            if (gm.lives == 0)
            {
                if (paddleAgent.isTraining)
                {
                    paddleAgent.EndEpisode();
                }
                else
                {
                    PlayerPrefs.SetInt("score", gm.score);
                    SceneManager.LoadScene("Game Over");
                }
            }
        }
    }

    // Function for bricks/destroyed bricks
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("yellowBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(1);
            paddleAgent.brickDestroyed();
        }
        else if (collision.transform.CompareTag("greenBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(3);
            paddleAgent.brickDestroyed();
        }
        else if (collision.transform.CompareTag("orangeBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(5);
            paddleAgent.brickDestroyed();
        }
        else if (collision.transform.CompareTag("redBrick"))
        {
            Destroy(collision.gameObject);
            gm.updateScore(7);
            paddleAgent.brickDestroyed();
        }
        else if (collision.transform.CompareTag("Player"))
        {
            paddleAgent.ballDeflected();
        }
    }
}
