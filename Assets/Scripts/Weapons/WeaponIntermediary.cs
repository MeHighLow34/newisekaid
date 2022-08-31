using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class WeaponIntermediary : MonoBehaviour
    {
        [Header("Intermediary")]
        public PlayerManager playerManager;
        public PhotonView playerPhotonView;
        public Health myHealth;
    }
}