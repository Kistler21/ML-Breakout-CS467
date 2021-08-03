using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

// Code referenced from https://www.youtube.com/watch?v=zPFU30tbyKs

public class Paddle_Agent : Agent
{
    Rigidbody2D ballRigidBody;
    public Transform ball;
    public Transform bricks;
    public Ball_Script gameBall;
    public float speed;
    int numOfBricks;
    public GameObject bricksPrefab;
    public float rightScreenLimit;
    public float leftScreenLimit;
    public float pad_y;
    public float ball_y;
    public float ballPenalty = -1f;

    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        numOfBricks = bricks.childCount;
        pad_y = transform.position.y;
        ball_y = ballRigidBody.transform.position.y;
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }


    public void Actions(float[] padActions)
    {
        int padSpeed = (int)10f;
        int xNeg = (int)-7.8f;
        int xPos = (int)7.8f;
        int ballRelease = (int)padActions[0];
        int leftRight = (int)padActions[1];

        if (leftRight == 0)
        {
            transform.position += new Vector3(1 * Time.deltaTime * padSpeed, 0f, 0f);
        }

        else if (leftRight == 1)
        {
            transform.position += new Vector3(-1 * Time.deltaTime * padSpeed, 0f, 0f);
        }

        else
        {
            transform.position += new Vector3(0 * Time.deltaTime * padSpeed, 0f, 0f);
        }


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xNeg, xPos), transform.position.y, transform.position.z);

        if(ballRelease == 1 && gameBall.in_play == true)
        {
            AddReward(1f);
        }

        else if (gameBall.in_play == false)
        {
            AddReward(ballPenalty);
        }
    }



    void Update()
    {
        // if stmnt to prevent paddle from passing screen edge on left
        if (this.transform.localPosition.x < leftScreenLimit)
        {
            this.transform.localPosition = new Vector3(leftScreenLimit, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        // if stmnt to prevent paddle from passing screen edge on right
        if (this.transform.localPosition.x > rightScreenLimit)
        {
            this.transform.localPosition = new Vector3(rightScreenLimit, this.transform.localPosition.y, this.transform.localPosition.z);
        }


    }


    public override void OnEpisodeBegin()
    {
        if (numOfBricks == 0)
        {
            bricks = Instantiate(bricksPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform.parent).transform;
            numOfBricks = bricks.childCount;
        }
        ballRigidBody.velocity = Vector2.zero;
        ball.localPosition = this.transform.localPosition + new Vector3(0, 0.1f, 0);
        ballRigidBody.AddForce(Vector2.up * speed);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        // Positions
        // sensor.AddObservation(ball.localPosition);
        sensor.AddObservation(ballRigidBody.position.x);
        sensor.AddObservation(ballRigidBody.position.y);

        // sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(this.transform.position.x);
        sensor.AddObservation(this.transform.position.y);

        /* Ball velocity
        sensor.AddObservation(ballRigidBody.velocity.x);
        sensor.AddObservation(ballRigidBody.velocity.y);
        */
        sensor.AddObservation(ballRigidBody.angularVelocity / 360f);

        // Number of bricks left
        sensor.AddObservation(bricks.childCount);

        if (numOfBricks > 0)
        {
            // Direction to closest brick
            Transform closestBrick = bricks.GetChild(0);
            float distanceToClosestBrick = Vector3.Distance(this.transform.localPosition, closestBrick.localPosition);
            for (var i = 1; i < bricks.childCount; i++)
            {
                if (distanceToClosestBrick > Vector3.Distance(this.transform.localPosition, bricks.GetChild(i).localPosition))
                {
                    closestBrick = bricks.GetChild(i);
                    distanceToClosestBrick = Vector3.Distance(this.transform.localPosition, closestBrick.localPosition);
                }
            }
            Vector3 directionToClosestBrick = this.transform.localPosition - closestBrick.localPosition;
            sensor.AddObservation(directionToClosestBrick);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
        }
    }


    public float paddleSpeed = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 1
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        this.transform.Translate(controlSignal * paddleSpeed * Time.deltaTime);

        if (bricks.childCount < numOfBricks)
        {
            SetReward(0.5f * (numOfBricks - bricks.childCount));
            numOfBricks = bricks.childCount;
            if (numOfBricks == 0)
            {
                EndEpisode();
            }
        }

        if (ball.localPosition.y < this.transform.localPosition.y - 1 ||
            (ball.localPosition.x > 10 || ball.localPosition.x < -10) ||
            ball.localPosition.y > 4.5)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

}


