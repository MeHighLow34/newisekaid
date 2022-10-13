using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class EnemyAnimationEvents : MonoBehaviour
    {
        public Animator animator;

        [Header("Event Objects")]
        public Transform rightHand;
        public Transform leftHand;

        private void Awake()
        {
            animator = GetComponent<Animator>();    
        }


        // Animation Events

        public void DisableAttack(AnimationEvent animationEvent)
        {
            animator.SetBool(animationEvent.stringParameter, false);
        }


        public void EnableWeapon()
        {
            var weapon = rightHand.GetComponentInChildren<EnemyWeaponDetector>();
            weapon.gameObject.GetComponent<Collider>().enabled = true;
        }

        public void DisableWeapon()
        {
            var weapon = rightHand.GetComponentInChildren<EnemyWeaponDetector>();
            weapon.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
