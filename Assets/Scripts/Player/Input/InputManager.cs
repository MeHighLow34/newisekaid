using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class InputManager : MonoBehaviour
    {
        PlayerActions playerActions;
        [Header("Bools")]
        public bool lightAttack;
        public bool comboFlag;
        // Dependencies
        PlayerAttacker playerAttacker;
        PlayerManager playerManager;

        private void Awake()
        {
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
            HandleAttackInput();
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
