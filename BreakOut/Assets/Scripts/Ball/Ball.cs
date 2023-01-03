using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BaseBehaviour // The ball that bounces in the level hit by the paddle
{
    private float speed = 2.8f;
    private float xOffset = 0.3f; // Tilt the ball a bit to make it not just go straight up
    private bool launched = false; // Did the player launch the ball from the paddle
    private bool died = false; // Did the ball fall down
    private BallPhysics ballPhysics = null;
   

    public void LaunchBall()
    {

        GetBallPhysics().GetRigidBody().velocity = new Vector2(xOffset * RandomSign(), speed);
        launched = true;
        died = false;
    }

    public void StopBall()
    {
        GetBallPhysics().GetRigidBody().velocity = new Vector2(0, 0);
        launched = false;
        died = false;
    }

    private BallPhysics GetBallPhysics()
    {
        if (ballPhysics == null)
        {
            ballPhysics = GetComponent<BallPhysics>();
        }
        return ballPhysics;
    }

    public bool IsLaunched()
    {
        return launched;
    }

    public bool IsDead()
    {
        return died;
    }
    public void ToggleDeath(bool isDead)
    {
        died = isDead;
    }

}
