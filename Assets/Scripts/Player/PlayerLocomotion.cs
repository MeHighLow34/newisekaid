using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class PlayerLocomotion : MonoBehaviour
    {
        public float dodgeStaminaCost;
        AnimationManager animationManager;
        Stamina stamina;
        private void Awake()
        {
            stamina = GetComponent<Stamina>();
            animationManager = GetComponent<AnimationManager>();
        }


        public void HandleDodge()
        {
            bool enoughStamina = stamina.ReduceStamina(dodgeStaminaCost);
            if (enoughStamina) {
                animationManager.animator.SetBool("stepBack", true);
                animationManager.animator.SetBool("isInteracting", true);
                animationManager.animator.applyRootMotion = true;
            }
        }
    }
}
