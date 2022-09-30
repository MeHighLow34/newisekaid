using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace LastIsekai
{
    public class ItemPickup : Interactable, IPunOwnershipCallbacks
    {
        public Item item;
        PhotonView photonView;
        bool wasPickedUp = false;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }
        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        void PickUp()
        {
            photonView.RequestOwnership();
            wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp) {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (wasPickedUp)
            {
                photonView.RequestOwnership();
            }
            if (photonView.IsMine && wasPickedUp)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
            PhotonNetwork.Destroy(gameObject);  
        }

        public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
