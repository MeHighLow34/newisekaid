using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class PlayerPicker : MonoBehaviour
    {
        public BoxCollider collider;
        private void OnTriggerStay(Collider other)
        {
           Interactable interactable = other.GetComponent<Interactable>();  
           if (interactable != null)
           {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    interactable.Interact();
                }                
           }
        }
    }
}
