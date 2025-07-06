using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : ScriptableObject
{
    public string itemName;
    public int index;
    public int max_stack;
    public Sprite icon;
    public GameObject prefab;

    public virtual int Use(Player player)
    {
        
        return 0;
    }

    public virtual void Equip(Player player)
    {
        
    }

    public virtual void Unequip(Player player)
    {
        
    }

}
