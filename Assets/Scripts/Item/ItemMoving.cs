using System.Collections.Generic;
using UnityEngine;

public class ItemMoving : MonoBehaviour
{
    private GameObject _player;

    public float speed;

    public float distance;

    private List<ItemInventory> _items;

    public bool selection;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _items = GameObject.Find("Main Camera").GetComponent<Inventory>().items;
    }

    private void Update()
    {
        if (_player == null) return;
        Vector2 playerPos = _player.transform.position;
        Vector2 coinPos = transform.position;
        if (!(Vector2.Distance(playerPos, coinPos) < distance)) return;
        var occupiedCells = 0;

        var noMaxInstances = false;
        foreach (ItemInventory item in _items)
        {
            if (item.id == 0)
                occupiedCells++;
            if (item.id != GetComponent<ItemTrigger>().itemID || !item.combination || item.count >= 4) continue;
            noMaxInstances = true;
            break;
        }

        if (occupiedCells > 0 || noMaxInstances)
            transform.position =
                Vector3.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
    }
}
