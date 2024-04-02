using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public GameObject currentCamera;

    public GameObject item;

    public int itemID;

    public float distance;

    public bool death = false;

    public float deathDiscarding;

    public Vector2 startPos;

    public DataBase data;

    public bool functionality;

    private List<ItemInventory> _items;

    private void Start()
    {
        currentCamera = GameObject.Find("Main Camera");
        _items = GameObject.Find("Main Camera").GetComponent<Inventory>().items;
    }

    private void Update()
    {
        if (death != true) return;
        if (Vector2.Distance(startPos, item.GetComponent<Transform>().position) > deathDiscarding)
        {
            item.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        var occupiedCells = 0;
        var noMaxInstances = false;
        foreach (var itemInventory in _items)
        {
            if (itemInventory.id == 0)
                occupiedCells++;
            if (itemInventory.id != GetComponent<ItemTrigger>().itemID || !itemInventory.combination || itemInventory.count >= 4) continue;
            noMaxInstances = true;
            break;
        }

        if (occupiedCells <= 0 && !noMaxInstances) return;
        currentCamera.GetComponent<Inventory>().SearchForSameItem(data.items[itemID], 1);
        currentCamera.GetComponent<Inventory>().Update();
        Destroy(gameObject);
    }
}
