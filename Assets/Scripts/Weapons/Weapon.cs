using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LastIsekai;
using Photon.Pun;


    [CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapons", order = 1)]
    public class Weapon : Item
    {
        WeaponManager[] allWeaponManagers;
        WeaponManager weaponManager;
        [Header("Weapon Attributes")]
        public GameObject prefab;
        public GameObject secondaryPrefab;
        public GameObject pickUpPrefab;
        public AnimatorOverrideController animatorOverrideController;

        public override void Use()
        {
            FindLocalWeaponManager();
            if (weaponManager.currentWeapon == this) return;  // If we have this weapon already equipped then we should not equip it again
            weaponManager.EquipWeapon(this);
        }

    public override void Drop()
    {
        FindLocalWeaponManager();
        PhotonNetwork.Instantiate(pickUpPrefab.name, weaponManager.dropPosition.position, Quaternion.identity);
        Debug.Log("I have printed already");
    }
    private void FindLocalWeaponManager()
        {
           allWeaponManagers = FindObjectsOfType<WeaponManager>();
           foreach (var manager in allWeaponManagers)
           {
            if (manager.gameObject.name == "LocalPlayerStructure")
            {
                weaponManager = manager;
            }
           }
        }
}

