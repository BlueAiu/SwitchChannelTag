using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    private float Move_speed;
    private Direction direction;

    private float Destroy_x;

    public void Initialize(float speed, Direction dir, Transform destroyPoint)
    {
        Move_speed = speed;
        direction = dir;

        Destroy_x = destroyPoint.position.x;
    }

    private void Update()
    {
        Vector3 movedir = direction == Direction.Left ? Vector3.left : Vector3.right;

        transform.Translate(movedir * Move_speed * Time.deltaTime);

        if(IsOutofScreen())
        {
            Destroy(gameObject);
            Debug.Log("cloud was destroy");
        }
    }

    private bool IsOutofScreen()
    {
        return direction == Direction.Left ? transform.position.x <= Destroy_x : transform.position.x >= Destroy_x;
    }

}
