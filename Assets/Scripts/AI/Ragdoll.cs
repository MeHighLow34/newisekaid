using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class Ragdoll : MonoBehaviour
    {
        private Rigidbody[] ragdollBodies;
        private Collider[] ragdollColliders;
        private CharacterJoint[] ragdollJoints;
        private Animator animator;
        public Transform ragdollHolder;
        public bool ragdollEnabled;

        private void Start()
        {
            animator = GetComponent<Animator>();
            ragdollBodies = ragdollHolder.GetComponentsInChildren<Rigidbody>();
            ragdollColliders = ragdollHolder.GetComponentsInChildren<Collider>();
            ragdollJoints = ragdollHolder.GetComponentsInChildren<CharacterJoint>();
        }

        private void Update()
        {
            if (ragdollEnabled)
            {
                EnableRagdoll();
            }
            else
            {
                DisableRagdoll();
            }
        }
        public void EnableRagdoll()
        {
            animator.enabled = false;
            foreach(Rigidbody rb in ragdollBodies)
            {
                rb.isKinematic = false;
            }
            foreach (Collider collider in ragdollColliders)
            {
                collider.enabled = true;
            }
            foreach (CharacterJoint characterJoint in ragdollJoints)
            {
                characterJoint.enableProjection = true;
            }
        }


        public void DisableRagdoll()
        {
            animator.enabled = true;
            foreach (Rigidbody rb in ragdollBodies)
            {
                rb.isKinematic = true;
            }
            foreach (Collider collider in ragdollColliders)
            {
                collider.enabled = false;
            }
        }
    }
}