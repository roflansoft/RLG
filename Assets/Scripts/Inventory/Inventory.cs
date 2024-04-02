using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase data;

    public List<ItemInventory> items = new ();

    public GameObject gameObjShow;

    public GameObject inventoryMainObject;
    
    public int maxCount;

    public Camera inventoryCamera;
    
    public EventSystem inventoryEventSystem;

    public int currentID;
    
    public ItemInventory currentItem;

    public RectTransform movingObject;
    
    public Vector3 offset;

    public GameObject backGround;

    public static bool IsOpenInventory;

    public int uniqueItemCount;

    [SerializeField] private GameObject menu;
    
    public void Start()
    {
        if(items.Count == 0)
        {
            AddGraphics();
        }

        backGround.SetActive(false);
        UpdateInventory();
        backGround.SetActive(!backGround.activeSelf);
        backGround.SetActive(!backGround.activeSelf);
        uniqueItemCount = 0;
    }

    public void Update() 
    {
        if(currentID != -1)
        {
            MoveObject();
        }

        if(Input.GetKeyDown(KeyCode.E) && currentID == -1 && !menu.activeSelf)
        {
            backGround.SetActive(!backGround.activeSelf);
            if(backGround.activeSelf)
            {
                UpdateInventory();
            }
            if (!backGround.activeSelf && !menu.activeSelf)
                IsOpenInventory = false;
            else
                IsOpenInventory = true;
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        if (item.combination == true)
        {
            for (var i = 0; i < maxCount; ++i)
            {
                if (items[i].id != item.id) continue;

                if (items[i].count >= 4) continue;
                
                items[i].count += count;

                if (items[i].count > 4)
                {
                    count = items[i].count - 4;
                    items[i].count = 4;
                }
                else
                {
                    count = 0;
                    i = maxCount;
                }
            }
        }
        
        if (count > 0)
        {
            for(var i = 0; i < maxCount; ++i)
            {
                if (items[i].id != 0) continue;
                
                AddItem(i, item, count);
                i = maxCount;
            }
        }
        UpdateInventory();
    }

    private void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;
        items[id].itemGameObj.GetComponent<MouseTest>().item = null;
        items[id].combination = item.combination;

        if (items[id].combination == false) items[id].count = 1;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else 
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    private void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;
        items[id].itemGameObj.GetComponent<MouseTest>().item = invItem.itemButton;
        items[id].combination = invItem.combination;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }


    private void AddGraphics() 
    {
        for (var i = 0; i < maxCount; ++i) 
        {
            var newItem = Instantiate(gameObjShow, inventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            var itemInventory = new ItemInventory();
            itemInventory.itemGameObj = newItem;

            var rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            var tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(itemInventory);
        }
    }

    public void UpdateInventory()
    { 
        for(var i = 0; i < maxCount; ++i)
        {
            if(items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            if (items[i].count > 0)
            {
                items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
                items[i].itemGameObj.GetComponent<MouseTest>().item = data.items[items[i].id].item;
                items[i].itemGameObj.GetComponent<MouseTest>().id = i;
            }
            else
            {
                items[i].itemGameObj.GetComponent<Image>().sprite = data.items[0].img;
                items[i].itemGameObj.GetComponent<MouseTest>().item = null;
                items[i].itemGameObj.GetComponent<MouseTest>().id = i;
                items[i].id = 0;
            }
        }
    }

    private void SelectObject()
    {
        if(currentID == -1)
        {
            if (items[int.Parse(inventoryEventSystem.currentSelectedGameObject.name)].id == 0)
            {
                return;
            }
            currentID = int.Parse(inventoryEventSystem.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            var itemInventory = items[int.Parse(inventoryEventSystem.currentSelectedGameObject.name)];

            if (currentItem.id != itemInventory.id || currentItem.combination == false)
            {
                AddInventoryItem(currentID, itemInventory);

                AddInventoryItem(int.Parse(inventoryEventSystem.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if(itemInventory.count + currentItem.count <= 4)
                {
                    itemInventory.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[itemInventory.id], itemInventory.count + currentItem.count - 4);

                    itemInventory.count = 4;
                }

                itemInventory.itemGameObj.GetComponentInChildren<Text>().text = itemInventory.count.ToString();

            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
        UpdateInventory();
    }

    private void MoveObject()
    {
        var pos = Input.mousePosition + offset;
        pos.z = inventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = inventoryCamera.ScreenToWorldPoint(pos);
    }

    private ItemInventory CopyInventoryItem(ItemInventory old)
    {
        var itemInventory = new ItemInventory();

        itemInventory.id = old.id;
        itemInventory.itemGameObj = old.itemGameObj;
        itemInventory.count = old.count;
        itemInventory.combination = old.combination;

        return itemInventory;
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    
    public GameObject itemGameObj;

    public int count;

    public bool combination;

    public GameObject itemButton;
}