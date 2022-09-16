using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;



namespace LastIsekai
{
    public class Meteor : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        public float speed = 0.0001f;
        public Vector3 playerDirection;
        public PhotonView fireWallPhotonView;
        public PhotonView playerPV;
        public GameObject child;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            fireWallPhotonView = GetComponent<PhotonView>();
        }
        private void Start()
        {
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (fireWallPhotonView.IsMine == playerPV.IsMine)
            {
                var playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
                // child.transform.rotation = Quaternion.LookRotation(playerDirection);    
                transform.rotation = Quaternion.LookRotation(playerDirection);
            }
        }

        private void Update()
        {
            navMeshAgent.Move(playerDirection * Time.deltaTime * speed);
        }
    }
}