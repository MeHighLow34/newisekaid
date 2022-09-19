using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class VisualEffect : MonoBehaviour
    {
        public GameObject chokedVFX;
        PhotonView view;
        Mediary mediary;
        private void Awake()
        {
            mediary = GetComponent<Mediary>();
            view = GetComponent<PhotonView>();
            chokedVFX.SetActive(false); 
        }



        public void ShowVisualEffect(int viewID)
        {
            view.RPC("ShowEffect", RpcTarget.All, viewID);
        }

        public void HideVisualEffect(int viewID)
        {
            view.RPC("HideEffect", RpcTarget.All, viewID);
        }
        [PunRPC]
        public void ShowEffect(int viewID)
        {
            if(mediary.mainPhotonView.ViewID == viewID)
            {
                chokedVFX.SetActive(true);
            }
        }

        [PunRPC]
        public void HideEffect(int viewID)
        {
            if (mediary.mainPhotonView.ViewID == viewID)
            {
                chokedVFX.SetActive(false);
            }
        }
    }
}