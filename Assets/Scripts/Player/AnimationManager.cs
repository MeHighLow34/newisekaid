using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class AnimationManager : MonoBehaviour
    {
        public Animator animator;
        public CharacterController characterController;
        Vector3 rootMotion;
        public void PlayTargetAnimation(string Animation, bool isInteracting)  // outdated/doesn't work with Photon
        {
            animator.SetBool("isInteracting", isInteracting);
            animator.applyRootMotion = isInteracting;
            animator.CrossFade(Animation, 0.2f);
        }
        #region Root Animation
        private void OnAnimatorMove()
        {
            if(animator.applyRootMotion == true)
            {
                rootMotion += animator.deltaPosition;
            }
        }
        
        private void LateUpdate()
        {
            if (animator.applyRootMotion == true)
            {
                characterController.Move(rootMotion * Time.deltaTime);
                rootMotion = Vector3.zero;
            }
        }

        #endregion
    }
}