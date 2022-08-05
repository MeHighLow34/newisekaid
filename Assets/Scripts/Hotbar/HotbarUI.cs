using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class HotbarUI : MonoBehaviour
    {
        Hotbar hotbar;
        public Transform hotbarParent;
        HotbarSlot[] slots;

        private void Start()
        {
            slots = hotbarParent.GetComponentsInChildren<HotbarSlot>();
            hotbar = FindObjectOfType<Hotbar>();
            hotbar.onAbilityAddedCallback += UpdateUserInterface;
        }

        void UpdateUserInterface()
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if(i < hotbar.abilities.Count)
                {
                    slots[i].AddAbility(hotbar.abilities[i]);
                }
                else
                {
                    slots[i].Remove();
                }
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                slots[0].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                slots[1].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                slots[2].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                slots[3].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                slots[4].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                slots[5].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                slots[6].Use();
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                slots[7].Use();
            }
        }
    }
}
