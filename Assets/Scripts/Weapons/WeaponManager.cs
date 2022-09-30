using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

namespace LastIsekai
{
    public class WeaponManager : MonoBehaviourPunCallbacks
    {
        PhotonAnimatorView photonAnimatorView;
        PhotonView view;
        public Weapon currentWeapon;
        public Animator animator;
        public Transform handTransform;
        [Header("Unarmed/Default Weapon")]
        public Weapon unarmedWeapon;

        [Header("All Network Weapons")]
        public Weapon networkUnarmed;
        public Weapon networkCruger;
        public Weapon networkNuno;
        public Weapon networkHolySet;
        public Weapon networkClinge;
        [Header("Transform")]
        public Transform rightHandWeaponHolder;
        public Transform leftHandWeaponHolder;
        public Transform dropPosition;
        private void Awake()
        {
            view = GetComponentInParent<PhotonView>();
            photonAnimatorView = animator.gameObject.GetComponent<PhotonAnimatorView>();    
        }

        private void Start()
        {
            
            if (view.IsMine)
            {
                if (currentWeapon == null)
                {
                    EquipWeapon(unarmedWeapon);
                }
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            if (currentWeapon != null) UnequipWeapon();
            currentWeapon = weapon;
            if (weapon.animatorOverrideController != null) {
                animator.runtimeAnimatorController = currentWeapon.animatorOverrideController;        
            }
            if (weapon.prefab != null) Instantiate(weapon.prefab, handTransform);
            if (weapon.secondaryPrefab != null) Instantiate(weapon.secondaryPrefab, leftHandWeaponHolder);
            if (view.IsMine)
            {
                Hashtable hash = new Hashtable();
                hash.Add("Weapon", currentWeapon.name);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }
        }
        string weaponName;
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            weaponName = (string)changedProps["Weapon"];
            if(!view.IsMine && targetPlayer == view.Owner)
            {
                NetworkEquip(weaponName);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            EquipWeapon(currentWeapon); // re-equipping the weapon so it registers on client
        }

        public void NetworkEquip(string weaponName)
        {
            if (!view.IsMine)
            {
                if (weaponName == networkCruger.name)
                {
                    EquipWeapon(networkCruger);
                }
                if (weaponName == networkUnarmed.name)
                {
                    EquipWeapon(networkUnarmed);
                }
                if(weaponName == networkNuno.name)
                {
                    EquipWeapon(networkNuno);
                }
                if(weaponName == networkHolySet.name)
                {
                    EquipWeapon(networkHolySet);
                }
                if(weaponName == networkClinge.name)
                {
                    EquipWeapon(networkClinge);
                }
            }
        }
        [PunRPC]
        void ChangedWeapon()
        {
            print("I changed weapon");
        }
        public void UnequipWeapon()
        {
        //    Inventory.instance.Add(currentWeapon); // when we unequip the weapon we should add it back to the inventory
            WeaponDetector currentWeaponPrefab = handTransform.GetComponentInChildren<WeaponDetector>();
            WeaponDetector secondaryCurrentWeaponPrefab = leftHandWeaponHolder.GetComponentInChildren<WeaponDetector>();
            if(currentWeaponPrefab == null)
            {
                print("There is no prefab to delete ");
            }
            if (currentWeaponPrefab != null) Destroy(currentWeaponPrefab.gameObject);
            if (secondaryCurrentWeaponPrefab)
            {
                print("There is no secondary prefab to delete");
            }
            if (secondaryCurrentWeaponPrefab != null) Destroy(secondaryCurrentWeaponPrefab.gameObject);
        }


        
    }
}
