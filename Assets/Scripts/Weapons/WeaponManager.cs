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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if(weapon.animatorOverrideController != null) animator.runtimeAnimatorController = weapon.animatorOverrideController;
            Instantiate(weapon.prefab, handTransform);
        }

        public void UnequipWeapon()
        {

        }
    }
}
