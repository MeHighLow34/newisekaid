using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace LastIsekai
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyManager enemyManager;
        NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidBody;

        public LayerMask detectionLayer;
        public CharacterDetector currentTarget;

        public float distanceFromTarget;
        public float stoppingDistance= 0.5f;

        public float rotationSpeed = 15f;
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();    
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();  
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyRigidBody = GetComponent<Rigidbody>(); 
        }

        private void Start()
        {
            enemyRigidBody.isKinematic = false;
        }
        public void HandleDetection()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position,enemyManager.detectionRadius , detectionLayer);
            for(int i = 0; i < colliders.Length; i++)
            {
                CharacterDetector characterDetector = colliders[i].GetComponent<CharacterDetector>();   
                if(characterDetector != null)
                {
                    //CHECK FOR TEAM ID

                    Vector3 targetDirection = characterDetector.gameObject.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);    
                    if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        currentTarget = characterDetector;
                    }
                }
            }
        }

        public void HandleMoveToTarget()
        {
            if (enemyManager.isPerformingAction) return;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.gameObject.transform.position);
            Vector3 targetDirection = currentTarget.gameObject.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.gameObject.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            // if we are performing an action, stop our movement!!
            if (enemyManager.isPerformingAction)
            {
                enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromTarget > stoppingDistance)
                {
                    //  enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(currentTarget.gameObject.transform.position);
                    // Normalno juri svoj target
                }
                else if (distanceFromTarget <= stoppingDistance)
                {
                   // enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                    Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                    Vector3 targetVelocity = enemyRigidBody.velocity;

                    //navMeshAgent.enabled = false;
                   // navMeshAgent.SetDestination(currentTarget.gameObject.transform.position);
                   // enemyRigidBody.velocity = targetVelocity;
                    transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);

                    // kada stignemo gotovo

                }
            }

            HandleRotateTowardsTarget();
        }

        public void UpdateLocomotionAnimation()
        {
            enemyAnimatorManager.animator.SetFloat("Vertical", navMeshAgent.velocity.magnitude);
        }

        private void HandleRotateTowardsTarget()
        {
            if(enemyManager.isPerformingAction)
            {
                // rotate manually
                Vector3 direction = currentTarget.gameObject.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                if(direction == Vector3.zero)
                {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            }
            else // rotate with pathfindg (navmesh)
            {
            //    Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            //    Vector3 targetVelocity = enemyRigidBody.velocity;

             //   navMeshAgent.enabled = true;
            //    navMeshAgent.SetDestination(currentTarget.gameObject.transform.position);
             //   enemyRigidBody.velocity = targetVelocity;
              //  transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);

                //it already rotates with navmeshagent SINCE NAVMESHAGENT IS ON THE GAMEOBJECT SO NO NEED
            }
            
        }

    }
}