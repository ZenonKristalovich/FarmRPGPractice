using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Item> itemDatabase = new List<Item>();

    public void SpawnItem(Vector3 position, int index, int quantity, Vector2 randomDirection)
    {
        Item item = itemDatabase[index];
        Collectable itemObject = Instantiate(item.prefab, position, Quaternion.identity).GetComponent<Collectable>();
        
        // Set quantity
        itemObject.quantity = quantity;
        
        float throwForce = 1f;
        itemObject.GetComponent<Rigidbody2D>().AddForce(randomDirection * throwForce, ForceMode2D.Impulse);
    }
}
