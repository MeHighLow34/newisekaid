using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Ragdoll : MonoBehaviour
    {
        private PhotonView photonView;
        private Rigidbody[] ragdollBodies;
        private Collider[] ragdollColliders;
        private CharacterJoint[] ragdollJoints;
        private Animator animator;
        public Transform ragdollHolder;
        public bool ragdollEnabled;
        [Header("Exceptions")]
        public Collider exception1;
        public Collider exception2;
        public Collider exception3;
        public Collider exception4;


        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            animator = GetComponent<Animator>();
            ragdollBodies = ragdollHolder.GetComponentsInChildren<Rigidbody>();
            ragdollColliders = ragdollHolder.GetComponentsInChildren<Collider>();
            ragdollJoints = ragdollHolder.GetComponentsInChildren<CharacterJoint>();
            DisableRagdoll();
            HandleExceptions();
            GetComponent<CharacterCollison>().IgnoreCollision();
        }

        private void HandleExceptions()
        {
            if (exception1 != null) exception1.enabled = true;
            if(exception2 != null) exception2.enabled = true;
            if(exception3 != null) exception3.enabled = true;
            if (exception4 != null) exception4.enabled = true;
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
