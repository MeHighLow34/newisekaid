using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class PlayerLocomotion : MonoBehaviour
    {
        AnimationManager animationManager;

        private void Awake()
        {
            animationManager = GetComponent<AnimationManager>();
        }


        public void HandleDodge()
        {
            animationManager.animator.SetBool("stepBack", true);
            animationManager.animator.SetBool("isInteracting", true);
            animationManager.animator.applyRootMotion = true;
        }

       
    }
}
