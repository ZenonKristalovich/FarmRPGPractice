using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carrot Seeds", menuName = "Inventory/Carrot Seeds")]
public class Carrot_Seeds : Item
{
    
    public void Use()
    {
        Debug.Log("Plant Carrot Seeds");
    }
}
