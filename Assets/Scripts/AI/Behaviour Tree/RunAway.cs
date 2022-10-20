using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace LastIsekai
{
    public class RunAway : Action
    {
        public EnemyAnimatorManager enemyAnimatorManager;
        public EnemyManager enemyManager;
        public SharedTransform runAwayPosition;
        private Transform safePlace;
        private NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyManager = GetComponent<EnemyManager>();
            runAwayPosition.Value = enemyManager.runAwayPlace;
            safePlace = runAwayPosition.Value;
        }

        public override void OnStart()
        {
            enemyAnimatorManager.animator.SetBool("runAway", true);
            enemyManager.isPerformingAction = false; // so we don't rotate towards the target anymore
        }
        public override TaskStatus OnUpdate()
        {
            float distanceFromTarget = Vector3.Distance(safePlace.position, transform.position);
            navMeshAgent.SetDestination(safePlace.position);
            if(distanceFromTarget <= enemyManager.enemyLocomotionManager.stoppingDistance)
            {
                return TaskStatus.Success;
            }


            return TaskStatus.Running;
        }
    }
}