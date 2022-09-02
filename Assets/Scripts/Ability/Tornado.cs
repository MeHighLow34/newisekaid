using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Tornado : MonoBehaviour
    {
        public float speed = 15f;
        public Vector3 playerDirection;
        public PhotonView tornadoPhotonView;
        public PhotonView playerPV;

        private void Awake()
        {
            tornadoPhotonView = GetComponent<PhotonView>();
        }
        private void Start()
        {
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (tornadoPhotonView.IsMine == playerPV.IsMine)
            {
                var playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
            }
        }

        private void Update()
        {
            transform.position += playerDirection * speed * Time.deltaTime;
        }
    }
}