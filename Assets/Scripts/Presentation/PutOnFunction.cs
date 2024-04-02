using UnityEngine;
using UnityEngine.UI;

public class PutOnFunction : ItemFunction
{
    protected void PutPicture(string cellName, GameObject obj)
    {
        var cell = GameObject.Find(cellName);
        cell.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        cell.GetComponent<PersonButton>().item = obj;
    }

    protected void TakeOffPicture(string cellName)
    {
        var cell = GameObject.Find(cellName);
        cell.GetComponent<Image>().sprite = Resources.Load("ButtonBG", typeof(Sprite)) as Sprite;
        cell.GetComponent<PersonButton>().item = null;
        var itemTriger = GetComponent<ItemTrigger>();
        cell.GetComponent<PersonButton>().currentCamera.GetComponent<Inventory>().SearchForSameItem(itemTriger.data.items[itemTriger.itemID], 1);
    }
}
