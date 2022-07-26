using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

namespace LastIsekai
{
    public class Tornado : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        public float speed = 15f;
        public Vector3 playerDirection;
        public PhotonView tornadoPhotonView;
        public PhotonView playerPV;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
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
            navMeshAgent.Move(playerDirection * Time.deltaTime * speed);
        }
    }
}