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
        public float minViewable = 45f;
        public float maxViewable = 75f;
        Vector3 direction;
        private Camera cam;
        GameObject playerBody;
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
         
            if(beamPhotonView.IsMine == cam.GetComponentInParent<PhotonView>())
            {
                transform.rotation = Quaternion.LookRotation(cam.transform.forward);
            }
/*            Vector3 targetDirection;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetDirection = hit.point;
            }
            else
            {
                targetDirection = cam.transform.forward;
            }
            float viewAbleAngle = Vector3.Angle(targetDirection, playerBody.transform.forward);
            if (viewAbleAngle >= minViewable && viewAbleAngle <= maxViewable)
            {
                direction = cam.transform.forward;
            }
            else
            {
                direction = playerBody.transform.forward;
            }

            transform.rotation = Quaternion.LookRotation(direction);*/

        }
    }
}
