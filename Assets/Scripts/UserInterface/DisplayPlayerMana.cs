using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LastIsekai
{
    public class DisplayPlayerMana : MonoBehaviour
    {
        public Image effectImage;
        public Image image;
        public Mana mana;
        [SerializeField] float hurtSpeed = 0.005f;


        private void Start()
        {
            mana = FindObjectOfType<Mana>();
        }

        private void Update()
        {
            if(mana == null)
            {
                mana = FindObjectOfType<Mana>();
            }
            float speed = 10f;
            image.fillAmount = Mathf.Lerp(image.fillAmount, mana.GetDecimalValue(), Time.deltaTime * speed);
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