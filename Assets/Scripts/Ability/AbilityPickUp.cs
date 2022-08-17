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
        public PhotonView photonView;
        public bool wasPickedUp;
        public PlayerManager localPlayerManager;
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
            if(MeetsClassRequirments() == false)
            {
                print("You are not the correct class for this ability");
                return;
            }
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

        private bool MeetsClassRequirments()
        {
            WeaponManager weaponManager;
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    PlayerManager localPlayerManager = manager.gameObject.GetComponent<PlayerManager>();
                    if (localPlayerManager.playerClass == ability.playerClass) return true;
                }
            }
            return false;
        }
    }
}