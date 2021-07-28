using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

// Code referenced from https://www.youtube.com/watch?v=zPFU30tbyKs

public class Paddle_Agent : Agent
{
    Rigidbody2D rBody;
    Rigidbody2D ballrBody;
    public Transform ball;
    public Transform bricks;
    public float speed;
    int numOfBricks;
    public GameObject bricksPrefab;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        ballrBody = ball.GetComponent<Rigidbody2D>();
        numOfBricks = bricks.childCount;
    }

    public override void OnEpisodeBegin()
    {
        if (numOfBricks == 0)
        {
            bricks = Instantiate(bricksPrefab, new Vector3(0, 0, 0), Quaternion.identity).transform;
        }
        ballrBody.velocity = Vector2.zero;
        ball.localPosition = this.transform.localPosition + new Vector3(0, 1.0f, 0);
        ballrBody.AddForce(Vector2.up * speed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Positions
        sensor.AddObservation(ball.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Ball velocity
        sensor.AddObservation(ballrBody.velocity.x);
        sensor.AddObservation(ballrBody.velocity.y);

        // Number of bricks left
        sensor.AddObservation(bricks.childCount);
    }

    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 1
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, ball.localPosition);

        // Ball collided
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
        }

        if (bricks.childCount < numOfBricks)
        {
            numOfBricks = bricks.childCount;
            SetReward(5.0f);
            if (numOfBricks == 0)
            {
                EndEpisode();
            }
        }

        // Ball missed
        else if (ball.localPosition.y < this.transform.localPosition.y)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }
}
