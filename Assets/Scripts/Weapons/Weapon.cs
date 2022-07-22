using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapons", order = 1)]
    public class Weapon : Item
    {
        [Header("Weapon Attributes")]
        WeaponManager weaponManager;
        public GameObject prefab;
        public AnimatorOverrideController animatorOverrideController;
        [TextArea]
        public string description;

        public override void Use()
        {
            weaponManager = FindObjectOfType<WeaponManager>();
            if (weaponManager.currentWeapon == this) return;  // If we have this weapon already equipped then we should not equip it again
            weaponManager.EquipWeapon(this);
        }
    }
}
