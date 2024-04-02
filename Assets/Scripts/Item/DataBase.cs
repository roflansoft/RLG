using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public List<Item> items = new ();
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public Sprite img;
    public bool combination;
    public GameObject item;
}
