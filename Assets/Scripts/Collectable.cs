using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Item itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player)
        {
            Debug.Log("Item pick up");
            if(itemData != null)
            {
                if(player.inventory.Add(itemData))
                {
                    //Only destroy object if it was added to inventory
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
