using UnityEngine;

public class MouseTest : MonoBehaviour
{
    public GameObject item;
    
    public int id;
    
    public GameObject currentCamera;

    private bool _isMouseOver;

    private void Start()
    {
        currentCamera = GameObject.Find("Main Camera");
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
    }

    private void Update()
    {
        if (!_isMouseOver) return;
        
        if (!Input.GetMouseButtonDown(1)) return;
        
        if (item == null) return;
        
        if (item.GetComponent<ItemFunction>() == null) return;
        
        item.GetComponent<ItemFunction>().UseItem();
        currentCamera.GetComponent<Inventory>().items[id].count--;
        currentCamera.GetComponent<Inventory>().UpdateInventory();
    }
}
