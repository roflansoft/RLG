using UnityEngine;
using UnityEngine.UI;

public class ArmorFunction : ItemFunction
{
    public GameObject armor;

    public override void UseItem()
    {
        var body = GameObject.Find("Body");
        var gameManager = GameObject.Find("Game Manager");
        body.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        body.GetComponent<PersonButton>().item = armor;
        gameManager.GetComponent<PlayerSetts>().ChangeArmor(GetComponent<Armor>().armor);
    }

    public override void UnUseItem()
    {
        var body = GameObject.Find("Body");
        var gameManager = GameObject.Find("Game Manager");
        body.GetComponent<Image>().sprite = Resources.Load("ButtonBG", typeof(Sprite)) as Sprite;
        body.GetComponent<PersonButton>().item = null;
        var item_triger = GetComponent<ItemTrigger>();
        body.GetComponent<PersonButton>().currentCamera.GetComponent<Inventory>().SearchForSameItem(item_triger.data.items[item_triger.itemID], 1);
        gameManager.GetComponent<PlayerSetts>().ChangeArmor(-GetComponent<Armor>().armor);
    }
}
