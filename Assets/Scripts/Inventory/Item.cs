using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace LastIsekai
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item", order = 1)]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon;
        [TextArea]
        public string description;
        public bool isEssential;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }

        public virtual void Drop()
        {
            Debug.Log("I should be dropped");
        }
    }
}