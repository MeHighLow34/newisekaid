using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace LastIsekai
{
    public class SpawnPlayers : MonoBehaviour
    {
        public GameObject playerPrefab;
        public Transform spawnPosition;

        private void Start()
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity);
        }

    }
}