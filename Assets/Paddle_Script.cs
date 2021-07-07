using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Script : MonoBehaviour
{
    // variable for asset/sprite speed
    public float speed;

    // variables for edges of screen to prevent paddle from going off screen
    public float rightScreenLimit;
    public float leftScreenLimit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // variable for horizontal paddle movement value
        float horizontal = Input.GetAxis ("Horizontal");

        // function to move paddle
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        // if stmnt to prevent paddle from passing screen edge on left
        if(transform.position.x < leftScreenLimit)
        {
            transform.position = new Vector2(leftScreenLimit, transform.position.y);
        }

        // if stmnt to prevent paddle from passing screen edge on right
        if (transform.position.x > rightScreenLimit)
        {
            transform.position = new Vector2(rightScreenLimit, transform.position.y);
        }

    }
}
