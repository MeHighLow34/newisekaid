using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

namespace LastIsekai
{
    public class InputManager : MonoBehaviour
    {
        ThirdPersonController thirdPersonController;
        PhotonView view;
        PlayerActions playerActions;
        [Header("Bools")]
        public bool lightAttack;
        public bool comboFlag;
        public bool dodgeFlag;
        // Dependencies
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;
        PlayerLocomotion playerLocomotion;
        private void Awake()
        {
            thirdPersonController = GetComponent<ThirdPersonController>();
            view = GetComponentInParent<PhotonView>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerManager = GetComponent<PlayerManager>();  
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }
        private void OnEnable()
        {
            if(playerActions == null)
            {
                playerActions = new PlayerActions();
                playerActions.Action.Attack.performed += ctx => lightAttack = true;
                playerActions.Action.Movement.performed += ctx => dodgeFlag = true;

            }
            playerActions.Enable();
        }

        private void OnDisable()
        {
            playerActions.Disable();
        }


        public void HandleAllInput()
        {
            if (view.IsMine)
            {
                HandleAttackInput();
                HandleDodgeInput();
            }
        }

        private void HandleAttackInput()
        {
            if (lightAttack)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleLightAttackCombo();
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting || playerManager.noInteracting || !thirdPersonController.Grounded)
                    {
                        lightAttack = false;
                        return;
                    }
                    playerAttacker.HandleLightAttack();
                }
            }
            
        }

        private void HandleDodgeInput()
        {
            if (dodgeFlag)
            {
                if (playerManager.noInteracting)
                {
                    dodgeFlag = false;
                    return;
                }

                playerLocomotion.HandleDodge();
                Debug.Log("I should dodge right fucking now");
                dodgeFlag = false;
            }
        }
    }
}
