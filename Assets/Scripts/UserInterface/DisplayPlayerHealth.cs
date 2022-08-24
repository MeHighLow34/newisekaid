using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LastIsekai
{
    public class DisplayPlayerHealth : MonoBehaviour
    {
        public Image effectImage;
        public Image image;
        public Health playerHealth;
        [SerializeField] float hurtSpeed = 0.005f;

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
            if(effectImage.fillAmount > image.fillAmount)
            {
                effectImage.fillAmount -= hurtSpeed;
            }
            else
            {
                effectImage.fillAmount = image.fillAmount;
            }
        }
    }
}
