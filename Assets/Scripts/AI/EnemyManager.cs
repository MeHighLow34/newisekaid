using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class EnemyManager : MonoBehaviour
    {
        EnemyAnimatorManager enemyAnimatorManager;
        public EnemyLocomotionManager enemyLocomotionManager;
        public bool isPerformingAction;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;

        private Rigidbody rb;

        [Header("A.I Settings")]
        public float detectionRadius = 20;
        public float minimumDetectionAngle = -50f;
        public float maximumDetectionAngle = 50f;

        public float currentRecoveryTime = 0;
        public float recoveryTime = 3f;

        public float rediscoverTimer = 5f;
        private float timeElapsed;

        [Header("//References//")]
        public bool oldAI;
        public EnemyHealth myHealth;
        public Transform runAwayPlace;

        // Test

        List<CharacterDetector> characters = new List<CharacterDetector>();
        CharacterDetector closestCharacter;

        private void Awake()
        {
           enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
           enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
           rb = GetComponent<Rigidbody>();  
        }


        private void Update()
        {
            HandleRecoveryTimer();
         //   HandleRepeatDetection();
        }
        private void FixedUpdate()
        {
            if (oldAI)
            {
              HandleCurrentAction();
            }
            rb.isKinematic = false;
            enemyLocomotionManager.navMeshAgent.enabled = true;
        }

        private void LateUpdate()
        {
            enemyLocomotionManager.UpdateLocomotionAnimation();
        }
        private void HandleCurrentAction()
        {
            if (enemyLocomotionManager.currentTarget != null)
            {
                enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.gameObject.transform.position, transform.position);
            }
            if(enemyLocomotionManager.currentTarget == null)
            {
                enemyLocomotionManager.HandleDetection();
            }
            else if(enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)
            {
                enemyLocomotionManager.HandleMoveToTarget();
            }else if(enemyLocomotionManager.distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
            {
                NewAttackTarget();
            }
        }

        private void HandleRepeatDetection()
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= rediscoverTimer)
            {
                timeElapsed = 0;
                RepeatDetection();
            }
        }

        public void RepeatDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLocomotionManager.detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterDetector characterDetector = colliders[i].GetComponent<CharacterDetector>();
                
                if (characterDetector != null)
                {
                    //CHECK FOR TEAM ID
                    characters.Add(characterDetector);
                }
            }
            float minDist = Mathf.Infinity;
            foreach(CharacterDetector character in characters)
            {
                float dist = Vector3.Distance(transform.position, character.transform.position);
                if(dist < minDist)
                {
                    closestCharacter = character;
                    minDist = dist;
                }
            }
            if(closestCharacter != enemyLocomotionManager.currentTarget)
            {
                enemyLocomotionManager.currentTarget = closestCharacter;
            }
        }

        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
                enemyLocomotionManager.HandleRotateTowardsTarget();
            }

            if(isPerformingAction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        public void GetNewAttack()
        {
            Vector3 targetsDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
            enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for(int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if(enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if(viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        
                        if(currentAttack != null)
                        {
                            return;
                        }

                        temporaryScore += enemyAttackAction.attackScore;

                        if(temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }

        }

        public void NewAttackTarget()
        {
            if (isPerformingAction) return;
            if(currentAttack == null)
            {
                GetNewAttack();
            }
            else
            {
                isPerformingAction = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                enemyAnimatorManager.PlayAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }
        private void AttackTarget()
        {
            if (isPerformingAction) return;
            isPerformingAction = true;
            currentRecoveryTime = recoveryTime;
            enemyAnimatorManager.PlayAnimation("Attack1", true);

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Gizmos.color = Color.blue; 
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }
    }
}
