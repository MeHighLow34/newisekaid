using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace LastIsekai
{
    public class GroundSlash : MonoBehaviour
    {
        public float speed = 15f;
        public Vector3 playerDirection;
        public PhotonView groundSlashPhotonView;
        public PhotonView playerPV;

        private void Awake()
        {
            groundSlashPhotonView = GetComponent<PhotonView>();
        }
        private void Start()
        {
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (groundSlashPhotonView.IsMine == playerPV.IsMine)
            {
                var playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
                transform.rotation = Quaternion.LookRotation(playerDirection);
            }
        }

        private void Update()
        {
            transform.position += playerDirection * speed * Time.deltaTime;
        }
    }
}