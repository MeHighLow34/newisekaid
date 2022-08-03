using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace LastIsekai
{
    public class DisplayUsername : MonoBehaviour
    {
        public PhotonView playerPV;
        public TextMeshProUGUI text;

        private void Start()
        {
            text.text = playerPV.Owner.NickName;
        }
    }
}