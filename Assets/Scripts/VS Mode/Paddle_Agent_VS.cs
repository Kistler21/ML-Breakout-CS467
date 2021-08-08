using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

// Code referenced from https://www.youtube.com/watch?v=zPFU30tbyKs

public class Paddle_Agent_VS : Agent
{
    Rigidbody2D paddleRigidBody;
    Rigidbody2D ballRigidBody;
    public Transform ball;
    public Transform bricks;
    public GameObject bricksPrefab;
    public float rightScreenLimit;
    public float leftScreenLimit;
    public Ball_Target_VS ball_target;
    public GM gm;
    public float paddleSpeed = 10;
    public bool isTraining = false;

    // Set rewards/penalties for learning - [-1, 1] range for values
    float ballMissedPenalty = -1.0f;
    float ballHeldPenalty = -0.05f;
    float ballInPlayReward = 0.05f;
    float brickDestroyedReward = 1.0f;
    float ballDeflectedReward = 1.0f;

    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Positions
        sensor.AddObservation(ball.localPosition.x);
        sensor.AddObservation(ball.localPosition.y);

        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.y);

        // Ball velocity
        sensor.AddObservation(ballRigidBody.velocity.x);
        sensor.AddObservation(ballRigidBody.velocity.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        int releaseBall = actionBuffers.DiscreteActions[0];
        int paddleMovement = actionBuffers.DiscreteActions[1];

        if (releaseBall == 1)
        {
            ball_target.playBall();
        }

        if (paddleMovement == 0)
        {
            this.transform.Translate(1 * paddleSpeed * Time.deltaTime, 0, 0);
        }
        else if (paddleMovement == 1)
        {
            this.transform.Translate(-1 * paddleSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            this.transform.Translate(0, 0, 0);
        }

        this.transform.localPosition = new Vector3(
            Mathf.Clamp(this.transform.localPosition.x, leftScreenLimit, rightScreenLimit),
            this.transform.localPosition.y,
            this.transform.localPosition.z
        );

        if (!ball_target.in_play)
        {
            AddReward(ballHeldPenalty);
        }
        else
        {
            AddReward(ballInPlayReward);
        }

        if (bricks.childCount == 0)
        {
            EndEpisode();
        }
    }

    public void ballMissed()
    {
        AddReward(ballMissedPenalty);
        EndEpisode();
    }

    public void brickDestroyed()
    {
        AddReward(brickDestroyedReward);
    }

    public void ballDeflected()
    {
        AddReward(ballDeflectedReward);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actions = actionsOut.DiscreteActions;

        if (Input.GetButtonDown("Jump"))
        {
            actions[0] = 1;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            actions[1] = 0;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            actions[1] = 1;
        }
        else
        {
            actions[1] = 2;
        }
    }
}


