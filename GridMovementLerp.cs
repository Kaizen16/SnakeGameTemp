// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GridMovementLerp: MonoBehaviour
// {
//     //Flags
//     public bool isMoving;
//     public bool allowInput = true;
//     private bool buffer = false;

//     //Main Values
//     public Vector3 moveDirection = Vector3.zero;
//     public Vector3 bufferedDirection = Vector3.zero;
//     public Vector3 origPos, targetPos, bufferedPos;
//     public float timeToMove = .2f;
//     private Vector3 nextStep, nextTarget;

//     void Start()
//     {
//         //Just to check if enabled or disabled
//     }
//     // Moving Snake Head Only (Player Control)
//     public void UpdatePlayerMovement()
//     {
    
//         if (allowInput)
//         {
//             if (Input.GetKeyDown(KeyCode.UpArrow) && moveDirection != Vector3.down)
//             {
//                 bufferedDirection = Vector3.up;
//             }
//             if (Input.GetKeyDown(KeyCode.RightArrow) && moveDirection != Vector3.left)
//             {
//                 bufferedDirection = Vector3.right;
//             } 
//             if (Input.GetKeyDown(KeyCode.DownArrow) && moveDirection != Vector3.up)
//             {
//                 bufferedDirection = Vector3.down;
//             }
//             if (Input.GetKeyDown(KeyCode.LeftArrow) && moveDirection != Vector3.right)
//             {
//                 bufferedDirection = Vector3.left;
//             }
            

//         }

//         // If not moving and a direction is set, move the player
//         if (!isMoving && bufferedDirection != Vector3.zero)
//         {

//             if (!buffer)
//             {
//                 moveDirection = bufferedDirection;
//             }
//             StartCoroutine(MovePlayer(moveDirection));
            
//         }
//     }

//     public IEnumerator MovePlayer(Vector3 direction)
//     {
//         isMoving = true;
//         float elapsedTime = 0;
//         Vector3 realTarget;

//         origPos = transform.position;

//         if (buffer)
//         {
//             targetPos = transform.position + direction;
//             realTarget = getTarget(direction);
//             Debug.Log("Target Thingy: " + realTarget);
//         }

//         else
//         {
//             targetPos = new Vector3(Mathf.Round((origPos.x + direction.x) * 2) / 2f, Mathf.Round((origPos.y + direction.y) * 2) / 2f, 0);
//             realTarget = targetPos;
//         }

//         while (elapsedTime < timeToMove)
//         {
//             if (Vector3.Distance(transform.position, realTarget) < 0.01f) break;
//             transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
//             elapsedTime += Time.deltaTime; 
//             yield return null;
//         }

//         transform.position = realTarget;
//         isMoving = false;
//     }

//     // Movement of snake body onwards
//     public void FollowPrevious(GameObject previous)
//     {
//         GridMovement prevMovement = previous.GetComponent<GridMovement>();

//         if (!isMoving && prevMovement.isMoving == true)
//         {
//             origPos = transform.position;

//             if(Mathf.Abs(prevMovement.origPos.x - origPos.x) > 3 || Mathf.Abs(prevMovement.origPos.y - origPos.y) > 3)
//                 targetPos = prevMovement.bufferedPos;
            
//             else
//                 targetPos = new Vector3(Mathf.Round(prevMovement.origPos.x * 2) / 2f, Mathf.Round(prevMovement.origPos.y * 2) / 2f, 0);

//             StartCoroutine(FollowMovement(targetPos));
//         }
//     }

//     private IEnumerator FollowMovement(Vector3 target)
//     {

//         if(this.GetComponent<Wrapping>().isDuplicate == true)
//             transform.position = new Vector3 (transform.position.x + timeToMove, transform.position.y, 0);
        

//         isMoving = true;
//         float elapsedTime = 0;
        

//         if (buffer)
//             targetPos = transform.position + moveDirection;
        
//         else
//             targetPos = target;
        

//         moveDirection = calculateDirection(target);
//         rotate(origPos, target);

//         while (elapsedTime < timeToMove)
//         {
//             if (Vector3.Distance(transform.position, target) < 0.01f) break;
//             transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
//             elapsedTime+= Time.deltaTime;
//             yield return null;
//         }

//         transform.position = target;
//         isMoving = false;
//     }



//     public void moveDuplicate(GameObject original, int indexInArray)
//     {
//         GridMovement originalMov = original.GetComponent<GridMovement>();

//         if (!isMoving)
//         {
//             // Set the duplicate's state to match the original's pre-wrap state
//             origPos = originalMov.origPos;
//             targetPos = originalMov.targetPos;
//             moveDirection = originalMov.moveDirection;

//             // Start moving the duplicate towards the same target
//             StartCoroutine(duplicateMovement(targetPos, indexInArray));
//         }
//     }

//     private IEnumerator duplicateMovement(Vector3 target, int indexInArray)
//     {
//         // Setting the transform to whatever the original was going to be at

//         if (moveDirection.x > 0)
//         {
//             // Moving Right
//             nextStep = new Vector3(timeToMove, 0, 0);
//             nextTarget = new Vector3(1, 0, 0);
//         }

//         else if (moveDirection.x < 0)
//         {
//             // Moving Left
//             nextStep = new Vector3(timeToMove * -1, 0, 0);
//             nextTarget = new Vector3(-1, 0, 0);
//         }
        
//         else if (moveDirection.y > 0)
//         {
//             // Moving Up
//             nextStep = new Vector3(0, timeToMove, 0);
//             nextTarget = new Vector3(0, 1, 0);
//         }
//         else if (moveDirection.y < 0)
//         {
//             // Moving Down
//             nextStep = new Vector3(0, timeToMove * -1, 0);         
//             nextTarget = new Vector3(0, -1, 0);
//         }
        
//         transform.position = transform.position + nextStep/2;
//         //Debug.Log($"Duplicate starts at position: {transform.position}");


//         // Ensure the duplicate starts at its current position
//         Vector3 startPos = transform.position;

//         isMoving = true;
//         float elapsedTime = 0;

//         while (elapsedTime < timeToMove)
//         {
//             // Lerp between the start position and the target position
//             transform.position = Vector3.Lerp(startPos, (startPos + nextTarget), (elapsedTime / timeToMove));
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         // Snap to the target position after movement
//         transform.position = target;
//         isMoving = false;
//     }


//     private void rotate(Vector3 origPos, Vector3 targetPos)
//     {
//         // If Moving Left
//         if (origPos.x > targetPos.x)
//             this.transform.rotation = Quaternion.Euler(0, 0, 270);
//         // If Moving Right
//         else if (origPos.x < targetPos.x)
//             this.transform.rotation = Quaternion.Euler(0, 0, 90);
//         // If Moving Down
//         else if (origPos.y > targetPos.y)
//             this.transform.rotation = Quaternion.Euler(0, 0, 0);
//         // If Moving Up
//         else if (origPos.y < targetPos.y)
//             this.transform.rotation = Quaternion.Euler(0, 0, 180);
        
//     }

//     private Vector3 calculateDirection(Vector3 target)
//     {
//         // Calculate the difference between the target position and the original position
//         Vector3 direction = target - origPos;


//         if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//         {
//             // The movement is primarily along the x-axis
//             direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
//         }
//         else
//         {
//             // The movement is primarily along the y-axis
//             direction = new Vector3(0, Mathf.Sign(direction.y), 0);
//         }

//         return direction;
//     }

//     private Vector3 getTarget(Vector3 direction) // either x or y
//     {
//         float targetX;
//         float targetY;
//         Vector3 targetVector;

//         if (Mathf.Abs(direction.x) > 0)
//         {
//             targetX = Mathf.Floor(targetPos.x) + direction.x/2;
//             targetY = origPos.y;
//         }
//         else
//         {
//             targetX = origPos.x;
//             targetY = Mathf.Floor(targetPos.y) + direction.y/2;
//         }
        
//         targetVector = new Vector3 (targetX, targetY, 0);

//         return targetVector;
//     }

//     public Vector3 getNext(Vector3 direction)
//     {
//         if (direction.x > 0)
//             nextStep = new Vector3(timeToMove, 0f, 0f);
//         else if (direction.x  < 0)
//             nextStep = new Vector3(timeToMove * -1f, 0f, 0f);
//         else if (direction.y > 0)
//             nextStep = new Vector3(0f, timeToMove, 0f);
//         else if (direction.y < 0)
//             nextStep = new Vector3(0f, timeToMove * -1f, 0f);

//         return nextStep;
//     }

//     public void printStatus(GameObject gameObject, int indexInArray)
//     {
//         Debug.Log($"{gameObject.name} is at Index Position: {indexInArray}");
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Wall"))
//         {
//             buffer = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Wall"))
//         {
//             buffer = false;
//         }
//     }
// }
