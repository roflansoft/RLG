using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsTrigger : MonoBehaviour
{
    public Camera cam; 

    public GameObject item;
    public float distance;

    public bool death = false;
    public float death_discarding;

    public Vector2 start_pos;

    public DataBase data;

    void Update()
    {
        if (death == true)
        {
            if (Vector2.Distance(start_pos, item.GetComponent<Transform>().position) > death_discarding)
            {
                item.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Movhe*");
        if (collision.CompareTag("Player"))
        {
            cam = Camera.current;
            cam.GetComponent<Inventory>().SearchForSameItem(data.items[1], 1);
            Destroy(gameObject);  
        }
    }
}
