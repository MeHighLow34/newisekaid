using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

namespace LastIsekai
{
    public class AnimationEvents : MonoBehaviour
    {
        [Header("Event Objects")]
        public PhotonView realPhotonView;
        public Animator animator;
        WeaponManager weaponManager;
        WeaponManager[] allWeaponManagers;
        PhotonView photonView;
        Stamina myStamina;
        public GameObject testOrb;
        public string aoeName;
        public PlayerBehaviour playerBehaviour;
        [Header("Capsule - Properties")]
        public CapsuleCollider damageCollider;
        public float damageColliderDefaultValue;
        public float damageColliderShrinkValue;

        [Header("Instantiation Transforms")]
        public Transform instantiationTransform;
        public Transform tornadoInstantiationTransform;
        public Transform beamInstantiationTransform;
        public Transform groundSlashInstantiationTransform;
        public Transform greatShieldInstantiationTransform;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            photonView = GetLocalPhotonView();
            FindLocalWeaponManager();
            playerBehaviour = GetPlayerBehaviour(); 
            myStamina = FindObjectOfType<Stamina>();
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

        public void DisableDoCombo()
        {
            animator.SetBool("doCombo", false);
        }
        public void EnableNoInteracting()
        {
            animator.SetBool("noInteracting", true);
        }
        public void DisableNoInteracting()
        {
            animator.SetBool("noInteracting", false);
        }

        public void EnableMoving()
        {
            animator.SetBool("noMoving", false);
        }

        public void DisableMoving()
        {
            animator.SetBool("noMoving", true);
        }


        public void EnableAttack()
        {
            animator.SetBool("attack", true);
        }
        public void DisableAttack()
        {
            animator.SetBool("attack", false);
        }

        public void EnableShrink() // shrink referst to shrinking of damage collider i.e. when rolling
        {
            damageCollider.center = new Vector3(-0.006078436f, damageColliderShrinkValue, -0.08601242f);
        }

        public void DisableShrink()
        {
            damageCollider.center = new Vector3(-0.006078436f, damageColliderDefaultValue, -0.08601242f);
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
                DisableBlocked();
            }
        }

        public void DisableBlocked() // this goes directly on the DisableWeaponCollider because when we detect the shield we turn on blocked but at some point we will have to turn it off so this is the moment when the collider is disabled
        {
            if (photonView.IsMine)
            {
                var weaponDect = weaponManager.rightHandWeaponHolder.GetComponentInChildren<WeaponDetector>();
                weaponDect.blocked = false;
            }
        }

        public void DisableHit()
        {
            animator.SetBool("hit", false);
        }

        public void DisableImpact()
        {
            animator.SetBool("blockImpact", false); 
        }
        public void DisableDeath()
        {
            animator.SetBool("isDead", false);
        }

        public void ThrowOrb()
        {
           PhotonNetwork.Instantiate("Orb", instantiationTransform.position, Quaternion.identity);
        }

        public void AOEAttack()
        {
            if(aoeName == "FireWall")
            {
                PhotonNetwork.Instantiate(aoeName, groundSlashInstantiationTransform.position, Quaternion.identity);
            }
            else if(aoeName == "SwordFall")
            {
                PhotonNetwork.Instantiate(aoeName, groundSlashInstantiationTransform.position, Quaternion.identity);
            }
            else if(aoeName == "GreatShield")
            {
                if (realPhotonView.IsMine)
                {
                   var aoeGO = PhotonNetwork.Instantiate(aoeName, greatShieldInstantiationTransform.position, Quaternion.identity);   
                }
            }
            else if (aoeName == "GroundSlash")
            {
                PhotonNetwork.Instantiate(aoeName, groundSlashInstantiationTransform.position, Quaternion.identity);
            }
            else if(aoeName == "Tornado")
            {
                PhotonNetwork.Instantiate(aoeName, tornadoInstantiationTransform.position, Quaternion.identity);
            }
            else if (aoeName == "TestingelectroAbilityVFX")
            {
                PhotonNetwork.Instantiate(aoeName, transform.position + new Vector3(0, 12f, 0), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(aoeName, transform.position, Quaternion.identity);
            }
        }


        public void EffectGain()
        {
            if (realPhotonView.IsMine)
            {
                playerBehaviour.RageEnabled();
            }
        }

        public void ThrowEffect()
        {
            if (realPhotonView.IsMine)
            {
                var hook = PhotonNetwork.Instantiate("HookOBJ", tornadoInstantiationTransform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                hook.GetComponent<Hook>().caster = transform;
            }
        }

        public void BeamEffect()
        {
            if (realPhotonView.IsMine)
            {
                PhotonNetwork.Instantiate("Beam", beamInstantiationTransform.position, Quaternion.identity);
                playerBehaviour.BeamEnabled();
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

        private PlayerBehaviour GetPlayerBehaviour()
        {
            WeaponManager[] wholeWM = FindObjectsOfType<WeaponManager>();
            foreach (var manager in wholeWM)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponentInParent<PlayerBehaviour>();
                }
            }
            return null;
        }
    }
}
