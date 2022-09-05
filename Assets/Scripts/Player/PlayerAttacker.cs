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
        private void Awake()
        {
            photonView = GetComponentInParent<PhotonView>();    
            inputManager = GetComponent<InputManager>();
            animationManager = GetComponent<AnimationManager>();
        }

        public void HandleLightAttack()
        {
            animationManager.animator.SetBool("attack", true);
            animationManager.animator.SetBool("isInteracting", true);
            animationManager.animator.applyRootMotion = true;
            lastAttack = "Attack1";
        }
        public void HandleLightAttackCombo()
        {
            if (inputManager.comboFlag)
            {
                if(lastAttack == "Attack1")
                {
                    animationManager.animator.SetBool("doCombo", true);
                    animationManager.animator.SetBool("isInteracting", true);
                    animationManager.animator.applyRootMotion = true;
                    lastAttack = "Attack2";
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