using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hoe", menuName = "Inventory/Tools/Hoe")]
public class Hoe : Item
{

    public override int Use(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractBox interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
            if (interactBox != null)
            {
                Vector3Int position = interactBox.GetInteractBoxPosition();
                GameManager.instance.tileManager.PlowTile(position);
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
        InteractBox interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
        if (interactBox != null)
        {
            interactBox.interactBoxEnable();
        }
        else
        {
            Debug.LogWarning("InteractBox not found in player hierarchy");
        }
    }

    public override void Unequip(Player player)
    {
        InteractBox interactBox = player.transform.Find("InteractBox").GetComponent<InteractBox>();
        if (interactBox != null)
        {
            interactBox.interactBoxDisable();
        }
        else
        {
            Debug.LogWarning("InteractBox not found in player hierarchy");
        }
    }
}
