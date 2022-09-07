using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;


namespace LastIsekai
{
    public class GreatShield : Shield
    {
        public Vector3 playerDirection;
        public PhotonView greatShieldPhotonView;
        public PhotonView playerPV;
        GameObject playerBody;
        public Vector3 normalPositionOffset;
        private void Awake()
        {
            greatShieldPhotonView = GetComponent<PhotonView>();
        }
        private void Start()
        {
            playerPV = GameObject.Find("LocalBody").GetComponent<Mediary>().mainPhotonView;
            if (greatShieldPhotonView.IsMine == playerPV.IsMine)
            {
                playerBody = playerPV.gameObject.GetComponent<Mediary>().playerBody;
                playerDirection = playerBody.transform.forward;
                transform.rotation = Quaternion.LookRotation(playerDirection);
            }
            if (playerPV.Owner == greatShieldPhotonView.Owner)
            {
                   animationManager = playerPV.GetComponent<Mediary>().animMan;
                   myHealth = playerPV.gameObject.GetComponent<Mediary>().healther;
            }
            else
            {
                var players = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Player(Clone)");

                foreach (var player in players)
                {
                    var playerPhotonView = player.gameObject.GetComponent<PhotonView>();
                    if (playerPhotonView.Owner == greatShieldPhotonView.Owner)
                    {
                       animationManager = playerPhotonView.gameObject.GetComponent<Mediary>().animMan;  
                       myHealth = playerPhotonView.gameObject.GetComponent<Mediary>().healther;
                    }
                }
            }
        }

        private void Update()
        {
            Vector3 rotatedOffset = playerBody.transform.rotation * normalPositionOffset;
            transform.position = playerBody.transform.position + rotatedOffset;
            transform.rotation = playerBody.transform.rotation;
            blocking = true;
            abilityShield = true;
        }

        private void LateUpdate()
        {
            blocking = true;
            abilityShield = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}