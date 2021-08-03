using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Target : MonoBehaviour
{
    // variable for physics
    public Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // initializing variable
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("yellowBrick"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("greenBrick"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("orangeBrick"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("redBrick"))
        {
            Destroy(collision.gameObject);
        }
    }
}
