using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LastIsekai
{
    public class EnemyAnimatorManager : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        EnemyLocomotionManager enemyLocomotionManager;
        public Animator animator;
        public float speed = 1.5f;
        private void Awake()
        {
            animator = GetComponent<Animator>();    
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }
        public void PlayTargetAnimation(string Animation, bool isInteracting) 
        {
            animator.SetBool("isInteracting", isInteracting);
            animator.applyRootMotion = isInteracting;
            animator.CrossFade(Animation, 0.2f);
        }

/*        private void OnAnimatorMove()
        {
*//*            float delta = Time.deltaTime;
            enemyLocomotionManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyLocomotionManager.enemyRigidBody.velocity = velocity * speed;*//*
        }*/
    }
}