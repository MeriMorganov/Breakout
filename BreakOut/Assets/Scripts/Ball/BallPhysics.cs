using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : BaseBehaviour
{
    private const float MIN_SPEED_THRESHOLD = 2.0f; // To prevent the ball from going really slow
    private const float MAX_SPEED_THRESHOLD = 3.0f;
    private float minSpeedMulti = 1.5f;
    private Rigidbody2D rigidbody2D;
    private Vector2 prevVelocity;
    private Ball ball = null;

    public void Start()
    {
        GetRigidBody();
    }
    public void FixedUpdate()
    {
        if (GetBall().IsLaunched())
        {
            prevVelocity = GetRBRangedVelocity();
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
            GetRigidBody().velocity = Vector2.Reflect(prevVelocity, contactPoint.normal);
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
}


