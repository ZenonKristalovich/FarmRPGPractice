using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe", menuName = "Inventory/Tools/Hoe")]
public class Item: ScriptableObject
{
    public string itemName;
    public int index;
    public int max_stack;
    public Sprite icon;
}
