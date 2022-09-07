using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Destructor : MonoBehaviour
    {
        public float duration;
        private float timeElapsed;


        private void Update()
        {
            timeElapsed += Time.deltaTime;
            if( timeElapsed >= duration)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}