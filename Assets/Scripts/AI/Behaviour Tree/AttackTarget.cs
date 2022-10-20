using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace LastIsekai
{
    public class AttackTarget : Conditional
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        public override void OnAwake()
        {
           enemyManager = GetComponent<EnemyManager>();
           enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }
        public override TaskStatus OnUpdate()
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
            return TaskStatus.Running;
        }
    }
}