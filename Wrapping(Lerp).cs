// using UnityEngine;
// using System.Collections;

// public class WrappingLerp : MonoBehaviour
// {
     
//     private bool cooldown = false;
//     public bool isDuplicate = false;

//     private Vector3 preWrapPos;

//     private void Start()
//     {
//         //Just so I have that tick box show up, it look coo
        
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {

//         string wall = collision.gameObject.name;

//         if (collision.CompareTag("Wall") && cooldown == false && isDuplicate == false)
//         {
//             // Creates Duplicate
//             GameObject newObject = Instantiate(gameObject);
//             //Debug.Log($"Duplicate created at position: {newObject.transform.position}");
//             newObject.GetComponent<Wrapping>().isDuplicate = true;
            
//             // Current Movement
//             int indexInArray = GameManager.Instance.snakeParts.IndexOf(this.gameObject);
//             GridMovement gridMovement = GetComponent<GridMovement>();

//             // Duplicate's Movement
//             GridMovement duplicateMovement = newObject.GetComponent<GridMovement>();
//             duplicateMovement.allowInput = false;
//             duplicateMovement.isMoving = false;

//             duplicateMovement.targetPos = gridMovement.targetPos;
//             duplicateMovement.moveDuplicate(this.gameObject, indexInArray);

//             //Logs
//             Debug.Log($"Current index: {indexInArray}, Wall: {wall}, Old Position: {transform.position}");
//             Debug.Log($"OrigPos: {gridMovement.origPos}");
//             Debug.Log($"TargetPos: {gridMovement.targetPos}");


//             gridMovement.bufferedPos = gridMovement.targetPos;
//             gridMovement.StopAllCoroutines(); // Stop the movement coroutine

//             preWrapPos = transform.position;

            

//             // If the colliding unit is the head
//             if (indexInArray == 0)
//             {
//                 float wrapDifferenceX = 1 - (preWrapPos.x % 1);
//                 float wrapFlooredX = Mathf.Floor(preWrapPos.x);
//                 float wrapDifferenceY = 1 - (preWrapPos.y % 1);
//                 float wrapFlooredY = Mathf.Floor(preWrapPos.y);

//                 switch (wall)
//                 {
//                     case "Right Wall":
//                         transform.position = new Vector3(((wrapFlooredX + wrapDifferenceX) * -1) + gridMovement.timeToMove/2 - 1, transform.position.y, 0);
//                         break;

//                     case "Left Wall":
//                         transform.position = new Vector3(((wrapFlooredX + wrapDifferenceX) * -1) - gridMovement.timeToMove/2 + 1, transform.position.y, 0);
//                         break;

//                     case "Top Wall":
//                         transform.position = new Vector3(transform.position.x, ((wrapFlooredY + wrapDifferenceY) * -1) + gridMovement.timeToMove/2 - 1, 0);
//                         break;

//                     case "Bottom Wall":
//                         transform.position = new Vector3(transform.position.x, ((wrapFlooredY + wrapDifferenceY) * -1) - gridMovement.timeToMove/2 + 1, 0);
//                         break;
//                 }
//             }


//             // If the colliding unit is body or tail 
//             else
//             {
//                 if (indexInArray > 0)
//                 {
//                     GameObject previous = GameManager.Instance.snakeParts[indexInArray - 1];

//                     // Align the body/tail to its preceding part
//                     Vector3 nextStep = previous.GetComponent<GridMovement>().getNext(gridMovement.moveDirection);
//                     Debug.Log($"Previous Transform: {previous.transform.position}, moveDirection: {gridMovement.moveDirection}, nextStep: {nextStep}");
//                     transform.position = previous.transform.position + nextStep/2 - gridMovement.moveDirection;
//                 }
//             }
//             // Debug.Log($"{wall} Triggered By {this.gameObject}");
//             Debug.Log($"Current index: {indexInArray}, Wall: {wall}, New Position: {transform.position}");


            

//             //Start the movement for rest of the body
//             if (indexInArray > 0 && preWrapPos == GameManager.Instance.snakeParts[indexInArray - 1].GetComponent<Wrapping>().preWrapPos)
//                 gridMovement.FollowPrevious(GameManager.Instance.snakeParts[indexInArray - 1]);


//             Debug.Log($"After Wrapping: Head Index: {indexInArray}");
//             Debug.Log($"Transform.Position: {transform.position}");
//             Debug.Log($"OrigPos: {gridMovement.origPos}");
//             Debug.Log($"TargetPos: {gridMovement.targetPos}");

//             gridMovement.isMoving = false;

//             cooldown = true;
//             StartCoroutine(resetCooldown());

//         }
        
//     }
    
    
//     private void OnTriggerExit2D(Collider2D other)
//     {
//         // Check if the duplicate leaves the grid
//         if (other.CompareTag("Grid") && isDuplicate)
//         {
//             Debug.Log("Duplicate has left the grid!");
//             Destroy(gameObject); // Deletes the duplicate object
//         }
//     }

//     private IEnumerator resetCooldown()
//     {
//         yield return new WaitForSeconds(0.1f);
//         cooldown = false;
//     }
// }
