using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;
namespace LastIsekai
{
    public class SpawnPlayers : MonoBehaviour
    {
        public static event Action onPlayerSpawn;
        public GameObject playerPrefab;
        public Transform spawnPosition;
        [Header("Assign")]
        public CanvasGroup hud;
        public CanvasGroup inventory;
        private void Start()
        {
           var Player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity);
           UIManager ui = Player.GetComponentInChildren<UIManager>();
           ui.hud = hud;
           ui.inventory = inventory;
           onPlayerSpawn?.Invoke();
          if (Player.GetPhotonView().IsMine)
          {
                Player.GetComponentInChildren<WeaponManager>().gameObject.name = "LocalPlayerStructure";
          }
        }

    }
}