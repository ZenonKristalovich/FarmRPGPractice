using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
    public class Slot: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI quantityText; 
        [SerializeField] public Image iconImage;

        public Item item;
        public int quantity;


        public Slot()
        {
            item = null;
            quantity = 0;
        }


        public void refresh()
        {
            //Set the visuals to what they should be
            if(item != null)
            {
                iconImage.sprite = item.icon;
            }
            iconImage.enabled = item != null;


            if( quantity <= 1)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = quantity.ToString();

            }
        }

        public void update_slot(Item change, int count)
        {
            item = change;
            quantity = count;
            refresh();
        }


        /*
        public bool IsEmpty
        {
            get
            {
                if(itemName == "" && count == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed){
                return true;
            }

            return false;
        }

        public void AddItem(Item item)
        {
            this.itemName = item.data.itemName;
            this.icon = item.data.icon;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.icon = icon;
            count++;
            this.maxAllowed = maxAllowed;
        }

        public void RemoveItem()
        {
            if(count > 0)
            {
                count--;

                if(count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
        */
    }
