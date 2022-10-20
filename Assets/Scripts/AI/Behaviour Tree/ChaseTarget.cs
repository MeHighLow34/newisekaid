using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace LastIsekai
{
    public class ChaseTarget : Action
    {
        public SharedTransform Target;

        // Custom Variables
        NavMeshAgent navMeshAgent;
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public Transform target;
        public override void OnAwake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }

        public override void OnStart()
        {
            target = Target.Value;
            enemyLocomotionManager.currentTarget = target.GetComponent<CharacterDetector>();
        }
        public override TaskStatus OnUpdate()
        {
            target = Target.Value;
            float distanceFromTarget = Vector3.Distance(target.gameObject.transform.position, transform.position);
            if (distanceFromTarget > enemyLocomotionManager.stoppingDistance)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(target.position);
                Vector3 targetDirection = target.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                // if we are performing an action, stop our movement!!
                if (enemyManager.isPerformingAction)
                {
                    enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                    navMeshAgent.enabled = false;
                    return TaskStatus.Failure;
                }
                else
                {
                    if (enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)
                    {
                        //  enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

                        navMeshAgent.enabled = true;
                        navMeshAgent.SetDestination(target.position);
                        // Normalno juri svoj target
                        return TaskStatus.Running;
                    }
                    else if (distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
                    {
                        // enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                        Vector3 targetVelocity = enemyLocomotionManager.enemyRigidBody.velocity;

                        //navMeshAgent.enabled = false;
                        // navMeshAgent.SetDestination(currentTarget.gameObject.transform.position);
                        // enemyRigidBody.velocity = targetVelocity;
                        transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, enemyLocomotionManager.rotationSpeed / Time.deltaTime);
                        // kada stignemo gotovo
                    }
                }
                enemyLocomotionManager.HandleRotateTowardsTarget();
            }
            #region Attack
            else if (distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
            {
                if (enemyManager.isPerformingAction) return TaskStatus.Failure;
                if (enemyManager.currentAttack == null)
                {
                    enemyManager.GetNewAttack();
                }
                else
                {
                    enemyManager.isPerformingAction = true;
                    enemyManager.currentRecoveryTime = enemyManager.currentAttack.recoveryTime;
                    enemyAnimatorManager.PlayAnimation(enemyManager.currentAttack.actionAnimation, true);
                    enemyManager.currentAttack = null;
                    return TaskStatus.Success;
                }
            }
            #endregion
            return TaskStatus.Running;
        }
    }
}
