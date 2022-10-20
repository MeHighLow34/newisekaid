using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace LastIsekai
{
    public class CheckLowHealth : Conditional
    {
        public EnemyHealth enemyHealth;
        public EnemyManager enemyManager;

        public override void OnAwake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyHealth = enemyManager.myHealth;
        }

        public override TaskStatus OnUpdate()
        {
            if(enemyHealth.GetDecimal() <= 0.4) //40% of health
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

    }
}