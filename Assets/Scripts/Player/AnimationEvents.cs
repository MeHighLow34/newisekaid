using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class AnimationEvents : MonoBehaviour
    {
        public Animator animator;
        WeaponManager weaponManager;
        WeaponManager[] allWeaponManagers;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            FindLocalWeaponManager();
        }
        public void EnableCombo()
        {
            animator.SetBool("canDoCombo", true);
        }
        public void ResetToDefault()
        {
            animator.applyRootMotion = false;
            animator.SetBool("isInteracting", false);
        }
        public void DisableCombo()
        {
            animator.SetBool("canDoCombo", false);
        }
        public void EnableNoInteracting()
        {
            animator.SetBool("noInteracting", true);
        }
        public void DisableNoInteracting()
        {
            animator.SetBool("noInteracting", false);
        }

        public void EnableAttack()
        {
            animator.SetBool("attack", true);
        }
        public void DisableAttack()
        {
            animator.SetBool("attack", false);
        }


        public void EnableWeaponCollider()
        {
            var weaponCollider = weaponManager.rightHandWeaponHolder.GetComponentInChildren<BoxCollider>();
            weaponCollider.enabled = true;
        }

        public void DisableWeaponCollider()
        {
            var weaponCollider = weaponManager.rightHandWeaponHolder.GetComponentInChildren<BoxCollider>();
            weaponCollider.enabled = false;
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
}
