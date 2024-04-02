using UnityEngine;
using UnityEngine.UI;

public class BowFunction : ItemFunction
{
    public override void UseItem()
    {
        GameObject.Find("Left arm").GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
