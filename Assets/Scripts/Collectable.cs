using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Item itemData;
    public int quantity = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player)
        {
            if(itemData != null)
            {
                if(player.inventory.Add(itemData, quantity))
                {
                    // Simply destroy the object
                    Destroy(gameObject);
                }
            }
        }
    }
}
