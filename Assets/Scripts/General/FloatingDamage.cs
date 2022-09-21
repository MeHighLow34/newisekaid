using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace LastIsekai
{
    public class FloatingDamage : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI text;
        public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);
        private void Start()
        {
            transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        }

        public void SetText(string newText)
        {
            if (newText == text.text) return; 
            text.text = newText;
        }

    }
}