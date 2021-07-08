using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4

public class Ball_Script : MonoBehaviour
{
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
            rb.AddForce(Vector2.up * speed);
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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("brick"))
        {
            Destroy(collision.gameObject);
        }
    }
}
