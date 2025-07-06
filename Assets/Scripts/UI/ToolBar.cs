using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public List<Slot> slots = new List<Slot>();
    public int selectedSlot = 0;
    public Player player;

    public void updateToolBar()
    {
        foreach(Slot slot in slots)
        {
            slot.refresh();
        }
    }

    public void Start()
    {
        foreach(Slot slot in slots)
        {
            slot.refresh();
        }
    }

    public void Update()
    {
        int tempSelectedSlot = selectedSlot;
        // Handle number keys 1-9
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                tempSelectedSlot = i;
            }
        }

        //Handle Mouse Click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if(slots[selectedSlot].item != null)
            {
                int result = slots[selectedSlot].item.Use(player);
                if(result != 0)
                {
                    player.inventory.Remove(selectedSlot, result);
                }
            }
        }

        // Handle scroll wheel
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta > 0) // Scrolling up
        {
            tempSelectedSlot = (tempSelectedSlot - 1 + 9) % 9;  
        }
        else if (scrollDelta < 0) // Scrolling down
        {
            tempSelectedSlot = (tempSelectedSlot + 1) % 9; 
        }

        // Update the selected slot
        if (tempSelectedSlot != selectedSlot)
        {
            // Deactivate highlight on previous slot
            slots[selectedSlot].transform.Find("Highlight").gameObject.SetActive(false);
            if(slots[selectedSlot].item != null)
            {
                slots[selectedSlot].item.Unequip(player);
            }

            // Update selected slot
            selectedSlot = tempSelectedSlot;
            // Activate highlight on new slot
            slots[selectedSlot].transform.Find("Highlight").gameObject.SetActive(true);
            if(slots[selectedSlot].item != null)
            {
                slots[selectedSlot].item.Equip(player);
            }
        }
        
    }

    public void updateSlot(int index, Slot slot)
    {
        slots[index].item = slot.item;
        slots[index].quantity = slot.quantity;
        slots[index].refresh();
    }


    
}
