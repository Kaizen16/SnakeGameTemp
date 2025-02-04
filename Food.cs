using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int counter;

    private void Start()
    {
        spawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        counter += 1;

        spawn();
    }

    private void spawn()
    {
        float rangeX = (Random.Range(-16, 16) + 0.5f);
        float rangeY = (Random.Range(-8, 8) + 0.5f);
        transform.position = new Vector3(rangeX, rangeY, 0);

    }

}
