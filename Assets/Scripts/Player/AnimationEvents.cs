using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class AnimationEvents : MonoBehaviour
    {
        public Animator animator;
        WeaponManager weaponManager;
        WeaponManager[] allWeaponManagers;
        PhotonView photonView;
        [Header("Event Objects")]
        public GameObject testOrb;
        public Transform instantiationTransform;
        public string aoeName;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            photonView = GetLocalPhotonView();
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
            if (photonView.IsMine)
            {
                var weaponCollider = weaponManager.rightHandWeaponHolder.GetComponentInChildren<BoxCollider>();
                weaponCollider.enabled = true;
            }
        }

        public void DisableWeaponCollider()
        {
            if (photonView.IsMine)
            {
                var weaponCollider = weaponManager.rightHandWeaponHolder.GetComponentInChildren<BoxCollider>();
                weaponCollider.enabled = false;
            }
        }


        public void ThrowOrb()
        {
           PhotonNetwork.Instantiate("Orb", instantiationTransform.position, Quaternion.identity);
        }

        public void AOEAttack()
        {
            if (aoeName == "electroAbilityVFX")
            {
                PhotonNetwork.Instantiate(aoeName, transform.position + new Vector3(0, 3.5f, 0), Quaternion.identity);

            }
            else
            {
                PhotonNetwork.Instantiate(aoeName, transform.position, Quaternion.identity);
            }
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

  
 
        private PhotonView GetLocalPhotonView()
        {
            WeaponManager[] wholeWM = FindObjectsOfType<WeaponManager>();
            foreach (var manager in wholeWM)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponentInParent<PhotonView>();
                }
            }
            return null;
        }
    }
}
