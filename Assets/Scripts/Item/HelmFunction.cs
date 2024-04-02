using UnityEngine;
using UnityEngine.UI;

public class HelmFunction : PutOnFunction
{
    public GameObject helm;

    public override void UseItem()
    {
        var head = GameObject.Find("Head");
        var gameManager = GameObject.Find("Game Manager");

        head.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        head.GetComponent<PersonButton>().item = helm;

        gameManager.GetComponent<PlayerSetts>().ChangeArmor(GetComponent<Helm>().armor);
    }

    public override void UnUseItem()
    {
        var head = GameObject.Find("Head");
        var gameManager = GameObject.Find("Game Manager");

        head.GetComponent<Image>().sprite = Resources.Load("ButtonBG", typeof(Sprite)) as Sprite;
        head.GetComponent<PersonButton>().item = null;
        var itemTriger = GetComponent<ItemTrigger>();
        head.GetComponent<PersonButton>().currentCamera.GetComponent<Inventory>()
            .SearchForSameItem(itemTriger.data.items[itemTriger.itemID], 1);
        gameManager.GetComponent<PlayerSetts>().ChangeArmor(-GetComponent<Helm>().armor);
    }
}
