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

    public Transform bricks;

    public GameObject bricksPrefab;
    public Transform explosionRed, explosionYellow, explosionGreen, explosionOrange;


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
            resetBall();
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

        if (bricks.childCount == 0)
        {
            rb.velocity = Vector2.zero;
            in_play = false;
            resetBall();

            Destroy(bricks.gameObject);
            bricks = Instantiate(
                bricksPrefab,
                this.transform.parent.position + new Vector3(0.2f, -0.16f, 0),
                Quaternion.identity,
                this.transform.parent
            ).transform;

            gm.increaseLevel();
            speed *= 1.2f;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Game Modes Screen");
        }

        if (gm.lives == 0 && gm.score < computerGM.score)
        {
            gameOver();
        }
    }

    public void resetBall()
    {
        transform.position = paddle.position;
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
                gameOver();
            }
            else if (gm.lives == 0)
            {
                isInputEnabled = false;
            }
        }
    }

    public void gameOver()
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
            Transform newExplosion = Instantiate(explosionYellow, transform.position, transform.rotation);
            Destroy(newExplosion.gameObject, 2.0f);
            Destroy(collision.gameObject);
            gm.updateScore(1);
        }
        else if (collision.transform.CompareTag("greenBrick"))
        {
            Transform newExplosion = Instantiate(explosionGreen, transform.position, transform.rotation);
            Destroy(newExplosion.gameObject, 2.0f);
            Destroy(collision.gameObject);
            gm.updateScore(3);
        }
        else if (collision.transform.CompareTag("orangeBrick"))
        {
            Transform newExplosion = Instantiate(explosionOrange, transform.position, transform.rotation);
            Destroy(newExplosion.gameObject, 2.0f);
            Destroy(collision.gameObject);
            gm.updateScore(5);
        }
        else if (collision.transform.CompareTag("redBrick"))
        {
            Transform newExplosion = Instantiate(explosionRed, transform.position, transform.rotation);
            Destroy(newExplosion.gameObject, 2.0f);
            Destroy(collision.gameObject);
            gm.updateScore(7);
        }
    }
}
