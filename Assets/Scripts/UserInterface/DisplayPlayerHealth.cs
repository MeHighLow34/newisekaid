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
        }

        private void Update()
        {
            float speed = 10f;
            image.fillAmount = Mathf.Lerp(image.fillAmount, playerHealth.GetDecimal(), Time.deltaTime * speed);
        }
    }
}
