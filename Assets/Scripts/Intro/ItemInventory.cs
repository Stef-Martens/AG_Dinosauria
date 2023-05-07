using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory
{
    public Sprite Image;
    public string Name;

    public ItemInventory(Sprite image, string name = "Empty")
    {
        Image = image;
        Name = name;
    }
}
