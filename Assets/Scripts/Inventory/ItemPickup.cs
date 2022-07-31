using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class ItemPickup : Interactable
    {
        public Item item;
        public override void Interact()
        {
            base.Interact();
            PickUp();
        }

        void PickUp()
        {
           // base.photonView.RequestOwnership();
            bool wasPickedUp = Inventory.instance.Add(item);
            if (wasPickedUp) {
             //   PhotonNetwork.Destroy(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
