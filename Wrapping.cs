using UnityEngine;
using System.Collections;

public class Wrapping : MonoBehaviour
{
    private bool cooldown = false;
    public bool isDuplicate = false;
    private Vector3 preWrapPos;

    private void Start()
    {
        //Just so I have that tick box show up, it looks cool
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string wall = collision.gameObject.name;

        if (collision.CompareTag("Wall") && cooldown == false && isDuplicate == false)
        {
            // Creates Duplicate
            GameObject newObject = Instantiate(gameObject);
            newObject.GetComponent<Wrapping>().isDuplicate = true;
            
            GridMovement gridMovement = GetComponent<GridMovement>();
            GridMovement duplicateMovement = newObject.GetComponent<GridMovement>();

            duplicateMovement.allowInput = false;
            duplicateMovement.isMoving = false;
            duplicateMovement.targetPos = gridMovement.targetPos;
            duplicateMovement.moveDuplicate(this.gameObject, indexInArray);

            gridMovement.bufferedPos = gridMovement.targetPos;
            gridMovement.StopAllCoroutines();

            preWrapPos = transform.position;
            
            if (indexInArray == 0)
            {
                float wrapFlooredX = Mathf.Floor(preWrapPos.x);
                float wrapFlooredY = Mathf.Floor(preWrapPos.y);

                switch (wall)
                {
                    case "Right Wall":
                        transform.position = new Vector3(-wrapFlooredX + 1, transform.position.y, 0);
                        break;
                    case "Left Wall":
                        transform.position = new Vector3(-wrapFlooredX - 1, transform.position.y, 0);
                        break;
                    case "Top Wall":
                        transform.position = new Vector3(transform.position.x, -wrapFlooredY + 1, 0);
                        break;
                    case "Bottom Wall":
                        transform.position = new Vector3(transform.position.x, -wrapFlooredY - 1, 0);
                        break;
                }
            }
            else
            {
                if (indexInArray > 0)
                {
                    GameObject previous = GameManager.Instance.snakeParts[indexInArray - 1];
                    Vector3 nextStep = previous.GetComponent<GridMovement>().getNext(gridMovement.moveDirection);
                    transform.position = previous.transform.position + nextStep / 2 - gridMovement.moveDirection;
                }
            }

            if (indexInArray > 0 && preWrapPos == GameManager.Instance.snakeParts[indexInArray - 1].GetComponent<Wrapping>().preWrapPos)
                gridMovement.FollowPrevious(GameManager.Instance.snakeParts[indexInArray - 1]);

            gridMovement.isMoving = false;
            cooldown = true;
            StartCoroutine(resetCooldown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grid") && isDuplicate)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator resetCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
