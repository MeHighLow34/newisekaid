using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class WeaponManager : MonoBehaviour
    {
        public Weapon currentWeapon;
        public Animator animator;
        public Transform handTransform;
        [Header("Unarmed/Default Weapon")]
        public Weapon unarmedWeapon;


        private void Start()
        {
            if(currentWeapon == null)
            {
                EquipWeapon(unarmedWeapon);
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if(weapon.animatorOverrideController != null) animator.runtimeAnimatorController = weapon.animatorOverrideController;
            if(weapon.prefab != null) Instantiate(weapon.prefab, handTransform);
        }

        public void UnequipWeapon()
        {

        }
    }
}
