using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LastIsekai
{
    public class ItemInfo : MonoBehaviour
    {
        public Image itemIcon;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemDescription;
        public Item currentItem;
        
        public void ShowItem(Item item)
        {
            currentItem = item;
            itemIcon.sprite = currentItem.icon;
            itemName.text = currentItem.name;
            itemDescription.text = currentItem.description;
        }


        public void Hide()
        {
            currentItem = null;
            itemIcon.sprite = null;
            itemName = null;
            itemDescription = null;
        }


        public void Use()
        {
            currentItem.Use();
            Hide();
        }

        public void Drop()
        {

        }
        
    }
}