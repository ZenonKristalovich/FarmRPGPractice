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
        foreach(Slot slot in slots )
        {
            slot.refresh();
        }
    }

    public bool Add(Item item)
    {

        int nullPos = -1;

        for(int i = 0; i < slots.Count;i++)
        {
            if(slots[i].item != null)
            {
                //Verify same type of item
                if (slots[i].item.itemName == item.itemName)
                {
                    if(slots[i].item.max_stack >= (slots[i].quantity + 1))
                    {
                        slots[i].item = item;
                        slots[i].quantity++;
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
            slots[nullPos].quantity++;
            return true;
        }


        return false;
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

    /*
    public void Add(Item item)
    {
        foreach( Slot slot in slots)
        {
            if(slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach( Slot slot in slots)
        {
            if(slot.itemName == "")
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    public void Remove(int index, int numToRemove )
    {
        if(slots[index].count >- numToRemove)
        {
            for(int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventory toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if(toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for( int i = 0; i < numToMove; i++)
            {
                toSlot.AddItem(fromSlot.itemName,fromSlot.icon,fromSlot.maxAllowed);
                fromSlot.RemoveItem();
            }
        }

    }
    */

}
