using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class PhysicsAdder : MonoBehaviour
    {
        Rigidbody rb;
        public Vector3 centerOfMass;
        public float fallSpeed;
        private void Start()
        {
            gameObject.AddComponent<Rigidbody>();
            if (centerOfMass != Vector3.zero)
            {
                rb = GetComponent<Rigidbody>();
                rb.centerOfMass = centerOfMass;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }

        private void FixedUpdate()
        {
            rb.AddForce(transform.forward * Time.deltaTime * fallSpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.25f);
        }
    }
}
