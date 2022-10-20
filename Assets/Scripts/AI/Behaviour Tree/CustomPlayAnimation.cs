using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace LastIsekai
{
    public class CustomPlayAnimation : Action
    {
        public string animationName;
        public bool isInteracting;
        private EnemyAnimatorManager enemyAnimatorManager;

        public override void OnAwake()
        {
           enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        }

        public override void OnStart()
        {
            enemyAnimatorManager.animator.SetBool("runAway", false);
            enemyAnimatorManager.PlayAnimation(animationName, isInteracting);
        }


    }
}