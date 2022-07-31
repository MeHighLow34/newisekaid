using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class PlayerAttacker : MonoBehaviour
    {
        InputManager inputManager;
        AnimationManager animationManager;
        public string lastAttack;
        private void Awake()
        {
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
    }
}