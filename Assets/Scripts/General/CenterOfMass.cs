using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace LastIsekai
{
    public class CenterOfMass : MonoBehaviour
    {
        public Vector3 centerOfMass;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.centerOfMass = centerOfMass;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 0.25f);
        }

    }
}
