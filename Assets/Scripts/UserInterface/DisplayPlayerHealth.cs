using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LastIsekai
{
    public class DisplayPlayerHealth : MonoBehaviour
    {
        public Image image;
        public Health playerHealth;


        private void Awake()
        {
            SpawnPlayers.onPlayerSpawn += FindSpawnedPlayer;
        }


        private void FindSpawnedPlayer()
        {
            playerHealth = FindObjectOfType<Health>();
            print(playerHealth);
        }

        private void Update()
        {
            image.fillAmount = playerHealth.GetDecimal();
        }
    }
}
