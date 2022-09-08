using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai
{
    public class DisplayStamina : MonoBehaviour
    {
        public Image effectImage;
        public Image image;
        public Stamina stamina;
        [SerializeField] float hurtSpeed = 0.005f;

        private void Start()
        {
            stamina = FindObjectOfType<Stamina>();
        }

        private void Update()
        {
            if (stamina == null)
            {
                stamina = FindObjectOfType<Stamina>();
            }
            float speed = 10f;
            image.fillAmount = Mathf.Lerp(image.fillAmount, stamina.GetDecimalValue(), Time.deltaTime * speed);
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