using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAliner : MonoBehaviour
{
    [SerializeField] Transform startingPosition;
    Transform nextPosition;

    [SerializeField] GameObject heartPrefab;

    int healthAmount;
    [SerializeField] int maxHealth;
    void Start()
    {

        Vector2 positionOffset = new Vector2(1, 0);
        for (int i = 0; i < maxHealth; i++)
        {
            Vector2 heartPosition = (Vector2)startingPosition.position + positionOffset * i;

            Instantiate(heartPrefab, heartPosition, Quaternion.identity, transform);
        }

        healthAmount = maxHealth;

    }
}
