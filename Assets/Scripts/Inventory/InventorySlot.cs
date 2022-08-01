using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai {
    public class InventorySlot : MonoBehaviour
    {
        Item item;
        public Image icon;
        public Button removeButton;
        ItemInfo itemInfo;
        RemoveWarning warning;

        private void Start()
        {
            itemInfo = FindObjectOfType<ItemInfo>();
            warning = FindObjectOfType<RemoveWarning>();
        }

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            if (newItem.isEssential == false)
            {
                removeButton.interactable = true;
            }
            if(newItem.isEssential == true)
            {
                removeButton.interactable = false;
            }
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            warning.Show(item);
        }

        public void UseItem()
        {
            if(item != null)
            {
              itemInfo.ShowItem(item);
            }
        }
    }
}
