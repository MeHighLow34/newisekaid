using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace LastIsekai
{
    public class CustomIdle : Action
    {
        public SharedTransform Target;
        public override TaskStatus OnUpdate()
        {
            if(Target != null)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Running;
        }
    }
}