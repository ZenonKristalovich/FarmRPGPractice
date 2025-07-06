using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carrot Seeds", menuName = "Inventory/Seeds/Carrot Seeds")]
public class Carrot_Seeds : Item
{
    public int objectIndex;
    
    public override int Use(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractBox interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
            if (interactBox != null)
            {
                Vector3Int position = interactBox.GetInteractBoxPosition();
                if (GameManager.instance.tileManager.IsPlowed(position))
                {
                    GameManager.instance.objectManager.SpawnObject(position, objectIndex);
                    return 1;
                }
            }
            else
            {
                Debug.LogWarning("InteractBox not found in player hierarchy");
            }
        }
        return 0;
    }

    public override void Equip(Player player)
    {
        
    }

    public override void Unequip(Player player)
    {
        
    }
}
