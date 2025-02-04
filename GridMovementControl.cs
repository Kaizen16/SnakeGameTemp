using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovementControl : MonoBehaviour
{
    private int indexInArray;

    void Start()
    {

        GameManager snakeManager = GameManager.Instance;
        GridMovement movement = GetComponent<GridMovement>();
        

        indexInArray = snakeManager.snakeParts.IndexOf(gameObject);
        Debug.Log($"{gameObject.name} is at Index Position: {indexInArray}");

        // Some Checks
        if (snakeManager == null)
        {
            Debug.LogError("GameManager instance is null. Ensure GameManager script is attached to an active GameObject.");
            return;
        }

        movement = GetComponent<GridMovement>();
        if (movement == null)
        {
            Debug.LogError("GridMovement not found on this GameObject.");
            return;
        }

        if (indexInArray - 1 < 0 || indexInArray - 1 >= snakeManager.snakeParts.Count)
        {
            Debug.LogError("Index is out of bounds for snakeParts.");
            return;
        }



        // movement.allowInput = false;
        // movement.isMoving = false;

        // movement.targetPos = snakeManager.snakeParts[indexInArray - 1].GetComponent<GridMovement>().origPos;
        // movement.moveDirection = snakeManager.snakeParts[indexInArray - 1].GetComponent<GridMovement>().moveDirection;



    }

    // void Update()
    // {
    //     // Some Before Checks
    //     Debug.Log($"Previous Movement of: {movement}");
    //     Debug.Log($"Previous TargetPos: {movement.targetPos}\nPrevious MoveDirection: {movement.moveDirection}");

    //     // Some After Checks
    //     Debug.Log($"Updated Movement of: {movement}");
    //     Debug.Log($"Updated TargetPos: {movement.targetPos}\nUpdated MoveDirection: {movement.moveDirection}");
    // }

}
