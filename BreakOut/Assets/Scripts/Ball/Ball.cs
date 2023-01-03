using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BaseBehaviour // The ball that bounces in the level hit by the paddle
{
    private float speed = 2.8f;
    private float xOffset = 0.3f;
    private bool launched = false;
    private bool died = false;
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
