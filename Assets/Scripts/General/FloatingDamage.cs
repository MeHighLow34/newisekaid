using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LastIsekai
{
    public class FloatingDamage : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);
        private void Start()
        {
           // text.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        }



        public void SetDamageText(int damage)
        {
            text.text = damage.ToString();  
        }

        public void SetText(string newText)
        {
            text.text = newText;
        }
    }
}