using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
namespace LastIsekai
{
    public class DetectTarget : Conditional
    {
        public LayerMask detectionLayer;
        public CharacterDetector currentTarget;
        public SharedTransform Target;

        public float detectionRadius = 20;
        public float minimumDetectionAngle = -50f;
        public float maximumDetectionAngle = 50f;

        Collider[] colliders;

        public override TaskStatus OnUpdate()
        {
            if(currentTarget != null || Target.Value != null) return TaskStatus.Success;
            colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterDetector characterDetector = colliders[i].GetComponent<CharacterDetector>();
                if (characterDetector != null)
                {
                    //CHECK FOR TEAM ID

                    Vector3 targetDirection = characterDetector.gameObject.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                    if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                    {
                        currentTarget = characterDetector;
                        Target.Value = currentTarget.gameObject.transform;
                        return TaskStatus.Success;
                    }
                }
            }
            return TaskStatus.Failure;
        }
    }
}
