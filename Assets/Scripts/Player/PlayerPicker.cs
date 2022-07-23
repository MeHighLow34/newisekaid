using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class PlayerPicker : MonoBehaviour
    {
        public BoxCollider collider;
        public bool input;

        #region Input
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                input = true;
            }
        }
        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (input)
            {
                Interactable interactable = other.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    input = false;
                }
                //else
                //{
                //    input = false;
                //}
            }
        }
    }
}
