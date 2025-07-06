using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Inventory: MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public GameObject inventoryPanel;
    public ToolBar toolBar;
    [SerializeField] private Canvas canvas;

    private static Slot draggedSlot;
    private static Image draggedIcon;
    

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventory();
        }
    }

    public void toggleInventory()
    {
        //Turn on/off the inventory canvas

        if(inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            refresh();
        }
    }

    public void refresh()
    {
        //Recheck every slot to make sure showing the correct details
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].refresh();
            if(i < 9)
            {
                toolBar.updateSlot(i, slots[i]);
            }
        }
    }

    public bool Add(Item item, int quantity)
    {
        int nullPos = -1;

        for(int i = 0; i < slots.Count;i++)
        {
            if(slots[i].item != null)
            {
                //Verify same type of item
                if (slots[i].item.itemName == item.itemName)
                {
                    if(slots[i].item.max_stack >= (slots[i].quantity + quantity))
                    {
                        slots[i].item = item;
                        slots[i].quantity += quantity;
                        refresh();
                        return true;
                    }
                }
            }
            else if(nullPos == -1)
            {
                nullPos = i;
            }
        }

        if (nullPos != -1)
        {
            slots[nullPos].item = item;
            slots[nullPos].quantity += quantity;
            refresh();
            return true;
        }

        return false;
    }

    public void Remove(int slot_num, int quantity)
    {
        slots[slot_num].quantity -= quantity;
        if(slots[slot_num].quantity <= 0)
        {
            slots[slot_num].update_slot(null,0);
        }
        refresh();
    }

    //Drag/Drop procedure
    public void SlotBeginDrag(int slot_num)
    {
        if(slots[slot_num].item != null)
        {
            draggedSlot = slots[slot_num];
            draggedIcon = Instantiate(draggedSlot.iconImage);
            draggedIcon.transform.SetParent(canvas.transform);
            draggedIcon.raycastTarget = false;
            draggedIcon.rectTransform.sizeDelta = new Vector2(50,50);

            MoveToMousePosition(draggedIcon.gameObject);
        }
        
    }

    public void SlotDrag()
    {
        if(draggedIcon != null)
        {
            MoveToMousePosition(draggedIcon.gameObject);
        }
    }

    public void SlotEndDrag()
    {
        if(draggedIcon != null)
        {
            Destroy(draggedIcon.gameObject);
            draggedIcon = null;
        }

    }

    public void SlotDrop(int slot_num)
    {
        if(draggedIcon == null)
        {
            return;
        }

        //Move Item
        Slot fromSlot = draggedSlot;
        Slot toSlot = slots[slot_num];

        // If dropping to the same slot, do nothing
        if (fromSlot == toSlot)
        {
            return;
        }

        if(toSlot.item != null)
        {
            if(toSlot.item.itemName == fromSlot.item.itemName)
            {
                int openSpace = toSlot.item.max_stack - toSlot.quantity;
                int toGive = fromSlot.quantity;
                
                if(openSpace < toGive)
                {
                    toSlot.quantity += openSpace;
                    fromSlot.quantity -= openSpace;
                }
                else
                {
                    toSlot.quantity += toGive;
                    fromSlot.update_slot( null, 0);
                }
                
            }
            else
            {
                Item toSwap = toSlot.item;
                int quantity = toSlot.quantity;
                toSlot.update_slot(fromSlot.item,fromSlot.quantity);
                fromSlot.update_slot( toSwap, quantity);
            }
        }
        else
        {
            toSlot.update_slot(fromSlot.item,fromSlot.quantity);
            fromSlot.update_slot( null, 0);
        }
        refresh();
    }

    public void dropFromInventory()
    {
        if(draggedIcon != null)
        {
            //Get Item to Drop
            Item toDrop = draggedSlot.item;

            //Spawn Item 
            Transform playerTransform = transform.root; // Get the root transform (player)
            
            // Generate random angle between 0 and 360 degrees
            float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            
            // Spawn item 1 unit away from player in random direction
            Vector2 spawnPosition = (Vector2)playerTransform.position + randomDirection;
            GameManager.instance.itemDB.SpawnItem(spawnPosition,toDrop.index,draggedSlot.quantity,randomDirection);
              
            //Destroy Icon
            Destroy(draggedIcon.gameObject);
            draggedIcon = null;
            draggedSlot.update_slot(null,0);
            draggedSlot = null;
            refresh();
        }
    }
    
    //End of Drag/Drop procedure

    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition,null,out position);
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

}
