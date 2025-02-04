using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public bool isMoving;
    public bool allowInput = true;
    public bool buffer = false;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 bufferedDirection = Vector3.zero;
    public Vector3 origPos, targetPos;
    public float speed = 2f;


    public Vector3 UpdatePlayerMovement()
    {
        if (allowInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && moveDirection != Vector3.down)
                bufferedDirection = Vector3.up;
            if (Input.GetKeyDown(KeyCode.RightArrow) && moveDirection != Vector3.left)
                bufferedDirection = Vector3.right;
            if (Input.GetKeyDown(KeyCode.DownArrow) && moveDirection != Vector3.up)
                bufferedDirection = Vector3.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && moveDirection != Vector3.right)
                bufferedDirection = Vector3.left;
        }


        if (!isMoving)
        {
            // Check if touching the wall (True if it is)
            if (!buffer)
                moveDirection = bufferedDirection;
        }
        return moveDirection;
    }

    private void Update()
    {
        var step =  speed * Time.deltaTime;
        moveDirection = UpdatePlayerMovement();

        if (!isMoving)
        {
            isMoving = true;
            origPos = transform.position;
            targetPos = transform.position + moveDirection;
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
    }
}