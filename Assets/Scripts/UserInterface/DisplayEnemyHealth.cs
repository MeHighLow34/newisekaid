using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai
{
    public class DisplayEnemyHealth : MonoBehaviour
    {
        public Image effectImage;
        public Image image;
        public EnemyHealth enemyHealth;
        [SerializeField] float hurtSpeed = 0.005f;



        private void Update()
        {
            float speed = 10f;
            image.fillAmount = Mathf.Lerp(image.fillAmount, enemyHealth.GetDecimal(), Time.deltaTime * speed);
            if (effectImage.fillAmount > image.fillAmount)
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
