using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
namespace LastIsekai
{
    public class ConnectToServer : MonoBehaviourPunCallbacks //gives us access to the callback function like the last 2
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()  //callback when we connect to server
        {
            PhotonNetwork.JoinLobby();
         }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("Lobby");
        }

    }
}