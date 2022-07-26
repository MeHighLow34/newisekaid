using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class PlayerPicker : MonoBehaviour
    {
        public BoxCollider collider;
        public bool input;
        [SerializeField] PhotonView photonView;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        #region Input
        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    input = true;
                }
            }
        }
        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (input && photonView.IsMine)
            {
                Interactable interactable = other.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    input = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            input = false;
        }
    }
}
