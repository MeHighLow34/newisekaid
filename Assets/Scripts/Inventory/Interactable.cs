using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Interactable : MonoBehaviourPun
    {
        public float radius = 2.5f;

        public virtual void Interact()
        {
            print("Interacting with " + gameObject.name);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}
