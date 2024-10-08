using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : BaseBehaviour // Checks and handles the physics side of the ball
{
    private const float MIN_SPEED_THRESHOLD = 2.0f; // To prevent the ball from going really slow
    private const float MAX_SPEED_THRESHOLD = 3.0f; // To prevent the ball from going really fast
    private float minSpeedMulti = 1.5f; // Speed up the ball by this multiplier when it's too slow
    private Rigidbody2D rigidbody2D;
    private Vector2 prevVelocity;
    private Vector2 pausedVelocity; //The last velocity when the player pauses
    private Ball ball = null;

    public void Start()
    {
        GetRigidBody();
    }
    public void FixedUpdate()
    {
        if (GameManager.Instance.GetGameMode() == GameManager.GameMode.Play)
        {
            if (GetBall().IsLaunched())
            {
                prevVelocity = GetRBRangedVelocity();
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetBall().IsLaunched())
        {

            ContactPoint2D contactPoint = collision.contacts[0];

            switch (collision.gameObject.tag)
            {
                case Tags.BRICK:
                    collision.gameObject.SetActive(false); //TODO keep count of bricks
                    LevelManager.Instance.NumOfBricks--;
                    break;
                case Tags.PADDLE:

                    break;

            }
            if (!LevelManager.Instance.CheckIfLevelFinished())
            {
                GetRigidBody().velocity = Vector2.Reflect(prevVelocity, contactPoint.normal);
            }
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetBall().IsLaunched())
        {

            if (collision.gameObject.tag == Tags.KILL_VOLUME)
            {
                GetBall().ToggleDeath(true);
            }
        }

    }
    public Rigidbody2D GetRigidBody()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        return rigidbody2D;
    }
    private Ball GetBall()
    {
        if (ball == null)
        {
            ball = GetComponent<Ball>();
        }
        return ball;
    }
    private Vector2 GetRBRangedVelocity()
    {
        if (Mathf.Abs(GetRigidBody().velocity.y) < MIN_SPEED_THRESHOLD)
        {
            GetRigidBody().velocity = new Vector2(GetRigidBody().velocity.x, GetRigidBody().velocity.y * minSpeedMulti);
        }

        if (GetRigidBody().velocity.y > MAX_SPEED_THRESHOLD)
        {
            GetRigidBody().velocity = new Vector2(GetRigidBody().velocity.x, MAX_SPEED_THRESHOLD);
        }
        else if (GetRigidBody().velocity.y < -MAX_SPEED_THRESHOLD)
        {
            GetRigidBody().velocity = new Vector2(GetRigidBody().velocity.x, -MAX_SPEED_THRESHOLD);
        }

        return GetRigidBody().velocity;
    }

    public void UpdateRBSleep() // Update the ball velocity based on if the player paused or not
    {
        if(GameManager.Instance.GetGameMode() == GameManager.GameMode.Pause)
        {
            pausedVelocity = GetRigidBody().velocity;
            GetRigidBody().Sleep();
        }
        else if(GameManager.Instance.GetGameMode() == GameManager.GameMode.Play)
        {
            GetRigidBody().WakeUp();
            GetRigidBody().velocity = pausedVelocity;
        }
    }
}


