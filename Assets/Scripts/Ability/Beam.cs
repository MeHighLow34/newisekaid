using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Beam : MonoBehaviour
    {
        public Vector3 playerDirection;
        public PhotonView beamPhotonView;
        public PhotonView playerPV;
        Vector3 direction;
        private Camera cam;
        GameObject playerBody;
        public float minViewAbleAngle = 75f;
        public float maxViewAbleAngle = 115f;
        private void Awake()
        {
            beamPhotonView = GetComponent<PhotonView>();
            cam = Camera.main;
        }
        private void Start()
        {
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (beamPhotonView.IsMine == playerPV.IsMine)
            {
                var playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
                transform.rotation = Quaternion.LookRotation(playerDirection);
            }
        }

        private void Update()
        {
            float viewAbleAngle = Vector3.Angle(cam.transform.forward, playerDirection);
            if(beamPhotonView.IsMine == cam.GetComponentInParent<PhotonView>())
            {
                if (viewAbleAngle <= maxViewAbleAngle && viewAbleAngle >= minViewAbleAngle)
                {
                    transform.rotation = Quaternion.LookRotation(cam.transform.forward);
                }
            }

        }
    }
}
