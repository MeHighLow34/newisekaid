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
    }
}