using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

namespace LastIsekai
{
    public class TestingKMKLDSF : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        public Vector3 playerDirection;
        public PhotonView groundSlashPhotonView;
        public PhotonView playerPV;
        public float speed;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
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
            navMeshAgent.Move(playerDirection * Time.deltaTime * speed);
           // navMeshAgent.SetDestination(transform.forward);
        }
    }
}
