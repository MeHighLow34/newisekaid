using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace LastIsekai
{
    public class AbilityPickUp : Interactable, IPunOwnershipCallbacks
    {
        public Ability ability;
        public Hotbar hotbar;
        PhotonView photonView;
        bool wasPickedUp;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();    
        }
        private void Start()
        {
            hotbar = FindObjectOfType<Hotbar>();
        }
        public override void Interact()
        {
            photonView.RequestOwnership();
            wasPickedUp = FindObjectOfType<Hotbar>().Add(ability);
            if(wasPickedUp)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (photonView.IsMine && wasPickedUp)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            throw new System.NotImplementedException();
        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
            throw new System.NotImplementedException();
        }

        public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}