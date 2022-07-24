using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class InputManager : MonoBehaviour
    {
        PhotonView view;
        PlayerActions playerActions;
        [Header("Bools")]
        public bool lightAttack;
        public bool comboFlag;
        // Dependencies
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;

        private void Awake()
        {
            view = GetComponentInParent<PhotonView>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerManager = GetComponent<PlayerManager>();  
        }
        private void OnEnable()
        {
            if(playerActions == null)
            {
                playerActions = new PlayerActions();
                playerActions.Action.Attack.performed += ctx => lightAttack = true;

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
                    if (playerManager.isInteracting || playerManager.noInteracting)
                    {
                        lightAttack = false;
                        return;
                    }
                    playerAttacker.HandleLightAttack();
                }
            }
            
        }
    }
}
