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
        public CanvasGroup canvasGroup;
        public WeaponManager weaponManager;
        public Button dropButton;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();  
        }
        public void ShowItem(Item item)
        {
            canvasGroup.alpha = 1;
            currentItem = item;
            print(currentItem);
            if(currentItem.icon != null) itemIcon.sprite = currentItem.icon;
            itemName.text = currentItem.name;
            itemDescription.text = currentItem.description;
            if (item.isEssential)
            {
                dropButton.interactable = false;
            }
            else
            {
                dropButton.interactable = true;
            }
        }


        public void Hide()
        {
            canvasGroup.alpha = 0;
            currentItem = null;
            itemIcon.sprite = null;
            itemName.text = null;
            itemDescription.text = null;
        }


        public void Use()
        {
            if (currentItem.isEssential == false && MeetsClassRequirments() == false)
            {
                Debug.Log("You do not meet the reqs");
                return;
            }
            if (currentItem.GetType() == typeof(Weapon) && currentItem != null)
            {
                weaponManager = GetLocalWeaponManager();
                Inventory.instance.Add(weaponManager.currentWeapon);
            }
            currentItem.Use();
            Inventory.instance.Remove(currentItem);
            Hide();
        }

        public void Drop()
        {
            Inventory.instance.Remove(currentItem);
            currentItem.Drop();
            Hide();
        }

        private WeaponManager GetLocalWeaponManager()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager;
                }
            }
            return null;
        }
        private bool MeetsClassRequirments()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    PlayerManager localPlayerManager = manager.gameObject.GetComponent<PlayerManager>();
                    if (localPlayerManager.playerClass == currentItem.playerClass) return true;
                }
            }
            return false;
        }
    }
}