using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using MoreMountains.Feedbacks;
namespace LastIsekai
{
    public class WeaponDetector : MonoBehaviour
    {
        // todo Actually we cannot since this is local instance which would defeat the whole purpose
        PlayerManager playerManager;
        public float damage = 1.5f;
        [Header("VFX")]
        public MMFeedbacks hitFeedback;
        public bool blocked;
        public int hitAnimation = 0;

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
                var enemyBody = other.GetComponent<Mediary>().playerBody;
                if(enemyBody != null)
                {
                    float directionHit = (Vector3.SignedAngle(playerManager.playerBody.transform.forward, enemyBody.transform.forward, Vector3.up));
                    if(directionHit >= 145 && directionHit <= 180)
                    {
                        hitAnimation = 0;
                    }else if(directionHit <= -145 && directionHit >= -180)
                    {
                        hitAnimation = 0;
                    }else if(directionHit >= -45 && directionHit <= 45)
                    {
                        hitAnimation = 1;
                    }else if(directionHit >= -144 && directionHit <= -45)
                    {
                        hitAnimation = 2;
                    }else if(directionHit >= 45 && directionHit <= 144)
                    {
                        hitAnimation = 3;
                    }
                    /*
                    0 je hit from front
                    1 je hit from back
                    2 je hit from left
                    3 je hit from right
                    */
                                        var enemyPV = other.GetComponent<PhotonView>();
                    var enemyHealth = other.GetComponent<Mediary>().healther;
                    enemyHealth.ChangeHitAnimation(hitAnimation, enemyPV.ViewID);
                }
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
