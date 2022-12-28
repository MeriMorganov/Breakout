using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : BaseBehaviour // PLayer controlled paddle used to hit the ball
{
    private float speed = 5.0f;
    public void MovePaddle()
    {
        float translation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(translation, 0,0);
    }

    public void Update()
    {
        MovePaddle();
    }
}
