using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class PlayerAttacker : MonoBehaviour
    {
        PhotonView photonView;
        public AnimationEvents animationEvents;
        InputManager inputManager;
        AnimationManager animationManager;
        public string lastAttack;
        Stamina stamina;
        WeaponManager weaponManager;
        private void Awake()
        {
            photonView = GetComponentInParent<PhotonView>();    
            inputManager = GetComponent<InputManager>();
            animationManager = GetComponent<AnimationManager>();
            stamina = GetComponent<Stamina>();  
            weaponManager = GetComponent<WeaponManager>();
        }

        public void HandleLightAttack()
        {
            bool enoughStamina = stamina.ReduceStamina(weaponManager.currentWeapon.attack1Cost);
            if (enoughStamina)
            {
                animationManager.animator.SetBool("attack", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
                lastAttack = "Attack1";
            }
            else
            {
                inputManager.lightAttack = false;
                print("I can't attack dude");
            }
        }
        public void HandleLightAttackCombo()
        {
            if (inputManager.comboFlag)
            {
                if(lastAttack == "Attack1")
                {
                    bool enoughStamina = stamina.ReduceStamina(weaponManager.currentWeapon.attack2Cost);
                    if (enoughStamina)
                    {
                        animationManager.animator.SetBool("doCombo", true);
                        animationManager.animator.SetBool("isInteracting", true);
                        animationManager.animator.applyRootMotion = true;
                        lastAttack = "Attack2";
                        return;
                    }
                    else
                    {
                        print("I  can't do combo since I don't have enough stamina");
                    }
                }
                if (lastAttack == "Attack2")
                {
                    bool enoughStamina = stamina.ReduceStamina(weaponManager.currentWeapon.attack3Cost);
                    if (enoughStamina)
                    {
                        animationManager.animator.SetBool("doCombo", true);
                        animationManager.animator.SetBool("isInteracting", true);
                        animationManager.animator.applyRootMotion = true;
                    }
                    else
                    {
                        print("I'M DEAD TIRED");
                    }
                }
            }
        }

        public void HandleAOE(string aoeName)
        {
            animationEvents.aoeName = aoeName;
            animationManager.animator.SetBool("aoeAttack", true);
            animationManager.animator.SetBool("isInteracting", true);
            animationManager.animator.applyRootMotion = true;
        }

        public void HandleThrowEffects()
        {
            if (photonView.IsMine)
            {
                animationManager.animator.SetBool("throw", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
            }
        }

        public void HandleBeamEffects()
        {
            if (photonView.IsMine)
            {
                animationManager.animator.SetBool("beam", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
            }
        }
        public void HandleGainEffects(string effectName)
        {
            if (photonView.IsMine)
            {
                animationManager.animator.SetBool("effectGain", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
            }
        }

      
    }
}