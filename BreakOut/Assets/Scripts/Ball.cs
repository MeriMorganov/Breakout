using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BaseBehaviour // The ball that bounces in the level hit by the paddle
{
    private float speed = 2.8f;
    private Rigidbody2D rigidbody2D;
    private Vector2 prevVelocity;

    public void Start()
    {
        GetRigidBody();
        GetRigidBody().velocity = new Vector2(speed, speed); //TODO just a test
    }
    public void FixedUpdate()
    {
        prevVelocity = GetRBVelocity();
    }

    public void OnCollisionEnter2D(Collision2D collision)
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

    private Rigidbody2D GetRigidBody()
    {
        if(rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        return rigidbody2D;
    }

    private Vector2 GetRBVelocity()
    {
        return GetRigidBody().velocity; 
    }
}
