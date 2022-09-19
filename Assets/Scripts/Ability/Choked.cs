using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Choked : MonoBehaviour
    {
        PhotonView photonView;
        public VisualEffect visualEffect;
        public GameObject playerBody;
        public CharacterController characterController;

        public float duration;
        private float timeElapsed;

        public float speed = 0.25f;
        public bool choked;

        private int currentViewID;
        private void Awake()
        {
         //   photonView = GetComponent<PhotonView>();
        }
        private void Update()
        {
            if (choked)
            {
                timeElapsed += Time.deltaTime;
                playerBody.transform.Translate(Vector3.up * speed * Time.deltaTime);    
                if (timeElapsed >= duration)
                {
                    choked = false;
                    characterController.enabled = true;
                    timeElapsed = 0;
                    visualEffect.HideVisualEffect(currentViewID);
                }
            }
        }
        public void Choke(int viewID)
        {
            choked = true;
            characterController.enabled = false;
            visualEffect.ShowVisualEffect(viewID);
            currentViewID = viewID; 
          //  photonView.RPC("ShowEffect", RpcTarget.All, photonView.ViewID);
        }

        [PunRPC]
        public void ShowEffect(int viewID)
        {
            if(photonView.ViewID == viewID)
            {
               // visualEffect.SetActive(true);
            }
        }
        
    }
}