using UnityEngine;

public class ColliderSizeChecker : MonoBehaviour
{
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            Debug.Log($"{gameObject.name} BoxCollider2D Size: {collider.size}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} does not have a BoxCollider2D!");
        }
    }
}
