using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMoving : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float distance;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 coinPos = transform.position;

            if (Vector2.Distance(playerPos, coinPos) < distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
