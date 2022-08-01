using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai
{
    public class RemoveWarning : MonoBehaviour
    {
        Item item;
        CanvasGroup canvasGroup;
        ItemInfo info;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            info = FindObjectOfType<ItemInfo>();
        }

        public void Show(Item itemToRemove)
        {
            info.Hide();
            item = itemToRemove;    
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            item = null;
            canvasGroup.alpha = 0;  
            canvasGroup.blocksRaycasts = false; 
        }
        public void Yes()
        {
            Inventory.instance.Remove(item);
            Hide();
        }

        public void No()
        {
            Hide();
        }
    }
}
