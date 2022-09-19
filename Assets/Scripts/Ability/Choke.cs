using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Choke : MonoBehaviour
    {
        public string tagCheck = "hookPlayer";

        [Header("Choke Across Network")]
        public PhotonView myPhotonView;
        public int collidedViewId;
        PhotonView localPV;

        private void Start()
        {
            localPV = GetComponent<PhotonView>();
            myPhotonView = GetLocalPhotonView();
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == tagCheck)
            {
                collidedViewId = other.gameObject.GetComponent<Mediary>().mainPhotonView.ViewID;
                GetComponent<PhotonView>().RPC("Choker", RpcTarget.All, collidedViewId);
            }
        }

        [PunRPC]
        private void Choker(int viewID)
        {
            if(myPhotonView.ViewID == viewID)
            {
                if (localPV.Owner == myPhotonView.Owner) return;
                Choked chokedOfChokedPlayer = myPhotonView.gameObject.GetComponent<Mediary>().choked;
                chokedOfChokedPlayer.Choke(viewID);
            }
        }


        private PhotonView GetLocalPhotonView()
        {
            WeaponManager[] wholeWM = FindObjectsOfType<WeaponManager>();
            foreach (var manager in wholeWM)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponentInParent<PhotonView>();
                }
            }
            return null;
        }

    }
}