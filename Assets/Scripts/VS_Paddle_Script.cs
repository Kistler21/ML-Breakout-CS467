using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4


public class VS_Paddle_Script : MonoBehaviour
{
    // variable for asset/sprite speed
    public float speed;

    // variables for edges of screen to prevent paddle from going off screen
    public float rightScreenLimit;
    public float leftScreenLimit;
    public float midScreenLimit;
    internal Vector3 position;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // variable for horizontal paddle movement value
        float horizontal = Input.GetAxis("Horizontal");

        // function to move paddle
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        // if stmnt to prevent paddle from passing screen edge on left
        if (transform.position.x < leftScreenLimit)
        {
            transform.position = new Vector2(leftScreenLimit, transform.position.y);
        }

        // if stmnt to prevent paddle from passing screen edge on right
        if (transform.position.x > rightScreenLimit)
        {
            transform.position = new Vector2(rightScreenLimit, transform.position.y);
        }

        //prevents paddles from crossing midpoint barrier
        if (transform.position.x == midScreenLimit)
        {
            transform.position = new Vector2(midScreenLimit, transform.position.y);
        }

    }
}
