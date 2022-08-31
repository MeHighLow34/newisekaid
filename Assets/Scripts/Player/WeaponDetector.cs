using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using MoreMountains.Feedbacks;
namespace LastIsekai
{
    public class WeaponDetector : MonoBehaviour
    {
        PlayerManager playerManager;
        public float damage = 1.5f;
        [Header("VFX")]
        public MMFeedbacks hitFeedback;
        public bool blocked;

        private void Start()
        {
            hitFeedback = GameObject.FindGameObjectWithTag("HitFEEDBACK").GetComponent<MMFeedbacks>();
            playerManager = GetLocalPlayerManager(); 
        }
        private void OnTriggerEnter(Collider other)
        {
            if (playerManager.attack == false) return; // if we are not attacking we don't take other players damage
            var blockCheck = other.GetComponent<Shield>();
            if(blockCheck != null)
            {
                if (blockCheck.blocking)
                {
                    blockCheck.BlockingReaction();
                    blocked = true;
                    return;
                }
            }
            if (blocked == false)
            {
                var victim = other.GetComponent<IDamageable>();
                if (victim == null) return;
                PhotonView victimPhotonView = other.GetComponent<PhotonView>();
                if (victimPhotonView.IsMine == false)
                {
                    victim.TakeDamage(damage);
                    hitFeedback.PlayFeedbacks();
                }
            }
        }

        private PlayerManager GetLocalPlayerManager()
        {
            WeaponManager[] wholeWM = FindObjectsOfType<WeaponManager>();
            foreach (var manager in wholeWM)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.gameObject.GetComponent<PlayerManager>();
                }
            }
            return null;
        }

    }
}
