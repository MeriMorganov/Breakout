using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : BaseBehaviour // Player controlled paddle used to hit the ball
{
    public GameObject ballObj;
    private Ball ball = null;
    private float speed = 5.0f;
    private int currentLives = GameManager.START_LIVES;
    private readonly Vector3 ballStartPos = new Vector3(0, 0.45f,0);
    private const float PADDLE_MIN_DIST = -2.26f;
    private const float PADDLE_MAX_DIST = 2.28f;


    public int CurrentLives
    {
        set
        {
            currentLives = Mathf.Clamp(value, 0, GameManager.MAX_LIVES);
        }
        get { return currentLives; }
    }

    public void Start()
    {
        UIManager.Instance.SetLivesValue(CurrentLives);
    }

    public void Update()
    {
        MovePaddle();
        UpdateBallStatus();
    }
    private void MovePaddle()
    {
        float translation = Input.GetAxis(Inputs.HORIZONTAL) * speed * Time.deltaTime;

        transform.Translate(translation, 0, 0);
        SetPaddleBoundry();
    }

    private void SetPaddleBoundry()
    {
        if (transform.position.x < PADDLE_MIN_DIST)
        {
            transform.position = new Vector3(PADDLE_MIN_DIST, transform.position.y, transform.position.z);
        }
        if (transform.position.x > PADDLE_MAX_DIST)
        {
            transform.position = new Vector3(PADDLE_MAX_DIST, transform.position.y, transform.position.z);
        }
    }
    private void UpdateBallStatus()
    {
        if (GetBall().IsLaunched() == false)
        {
            if (Input.GetKeyUp(Inputs.LAUNCH_BALL))
            {
                LaunchBall();
            }
        }
        else if (GetBall().IsDead() == true)
        {
            CurrentLives--;
            UIManager.Instance.SetLivesValue(CurrentLives);
            SetBallToPaddle();
        }
    }

    private void SetBallToPaddle()
    {
        GetBall().StopBall();
        ballObj.transform.parent = this.transform;
        ballObj.transform.localPosition = ballStartPos;
    }

    private void LaunchBall()
    {
        ballObj.transform.parent = null;
        GetBall().LaunchBall();
    }

    private Ball GetBall()
    {
        if(ball == null)
        {
            ball = ballObj.GetComponent<Ball>();
        }
        return ball;
    }
}
