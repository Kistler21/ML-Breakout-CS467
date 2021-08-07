using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4

public class Ball_Script : MonoBehaviour
{
    public Paddle_Agent paddle_agent;

    // variable for physics
    public Rigidbody2D rb;

    // variable for if ball is in play
    public bool in_play;

    // variable for paddle
    public Transform paddle;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // initializing variable
        rb = GetComponent<Rigidbody2D> ();
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
        if (Input.GetButtonDown ("Jump") && !in_play)
        {
            in_play = true;
            // force
            //rb.AddForce(Vector2.up * speed);

            //send ball in random direction (left or right)
           
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

            // Check if game over
            if (GM.lives == 0)
            {
                SceneManager.LoadScene("Game Over");
            }

            //Decrements life counter variable in GM script
            GM.lives--;
        }
    }

    // Function for bricks/destroyed bricks
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("yellowBrick"))
        {
            Destroy(collision.gameObject);
            GM.score += 1;
            paddle_agent.AddReward(1f);
        }
        else if (collision.transform.CompareTag("greenBrick"))
        {
            Destroy(collision.gameObject);
            GM.score += 3;
            paddle_agent.AddReward(1f);
        }
        else if (collision.transform.CompareTag("orangeBrick"))
        {
            Destroy(collision.gameObject);
            GM.score += 5;
            paddle_agent.AddReward(1f);
        }
        else if (collision.transform.CompareTag("redBrick"))
        {
            Destroy(collision.gameObject);
            GM.score += 7;
            paddle_agent.AddReward(1f);
        }
    }
}
