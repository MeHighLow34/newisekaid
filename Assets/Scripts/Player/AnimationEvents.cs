using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class AnimationEvents : MonoBehaviour
    {
        public Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
        public void EnableNoInteracting()
        {
            animator.SetBool("noInteracting", true);
        }
        public void DisableNoInteracting()
        {
            animator.SetBool("noInteracting", false);
        }

        public void EnableAttack()
        {
            animator.SetBool("attack", true);
        }
        public void DisableAttack()
        {
            animator.SetBool("attack", false);
        }
    }
}
