using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code derived from https://www.youtube.com/watch?v=NWG8vO02oj4


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
        float horizontal = Input.GetAxis("Horizontal");

        // function to move paddle
        transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftScreenLimit, rightScreenLimit),
            transform.position.y,
            transform.position.z
        );
    }
}
