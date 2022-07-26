using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LastIsekai;



    [CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapons", order = 1)]
    public class Weapon : Item
    {
        WeaponManager[] allWeaponManagers;
        WeaponManager weaponManager;
        [Header("Weapon Attributes")]
        public GameObject prefab;
        public AnimatorOverrideController animatorOverrideController;
        [TextArea]
        public string description;

        public override void Use()
        {
            allWeaponManagers = FindObjectsOfType<WeaponManager>();
        foreach (var manager in allWeaponManagers)
        {
             if (manager.gameObject.name == "LocalPlayerStructure")
             {
                weaponManager = manager;
             }
        }
            Debug.Log(weaponManager.name + "JEBEM TI SVE NA OVOME SVETU");
            if (weaponManager.currentWeapon == this) return;  // If we have this weapon already equipped then we should not equip it again
            weaponManager.EquipWeapon(this);
        }
    }

