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
        public float baseDamage;
        public BaseStats stats;
        [Header("VFX")]
        public GameObject floatingDamageText;
        public MMFeedbacks hitFeedback;
        public bool blocked;
        public int hitAnimation = 0;

        private void Start()
        {
            hitFeedback = GameObject.FindGameObjectWithTag("HitFEEDBACK").GetComponent<MMFeedbacks>();
            playerManager = GetLocalPlayerManager();
            stats = GetComponentInParent<Mediary>().baseStats;
            baseDamage = stats.GetStat(Stat.Damage);
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
            }  // if we detect a shield we won't do any damage
            if (blocked == false)
            {
                var victim = other.GetComponent<IDamageable>();
                if (victim == null) return;
                if (other.gameObject.tag == "PlayerHealth")
                {
                    var enemyBody = other.GetComponent<Mediary>().playerBody;
                    if (enemyBody != null)
                    {
                        float directionHit = (Vector3.SignedAngle(playerManager.playerBody.transform.forward, enemyBody.transform.forward, Vector3.up));
                        if (directionHit >= 145 && directionHit <= 180)
                        {
                            hitAnimation = 0;
                        } else if (directionHit <= -145 && directionHit >= -180)
                        {
                            hitAnimation = 0;
                        } else if (directionHit >= -45 && directionHit <= 45)
                        {
                            hitAnimation = 1;
                        } else if (directionHit >= -144 && directionHit <= -45)
                        {
                            hitAnimation = 2;
                        } else if (directionHit >= 45 && directionHit <= 144)
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
                        var floatingDamage = Instantiate(floatingDamageText, enemyHealth.bloodInstantiationPoint.position, Quaternion.identity);
                        floatingDamage.GetComponent<FloatingDamage>().SetText((baseDamage + damage).ToString());
                    }
                    PhotonView victimPhotonView = other.GetComponent<PhotonView>();
                    if (victimPhotonView.IsMine == false)
                    {
                        victim.TakeDamage(baseDamage + damage);
                        hitFeedback.PlayFeedbacks();
                    }
                } // if we detect other player
                if(other.gameObject.tag == "EnemyHealth")
                {
                    victim.TakeDamage(damage);
                }  // if we detect enemy ai
            }
        }

        #region Misc
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

        #endregion
    }
}
