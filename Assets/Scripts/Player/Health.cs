using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using MoreMountains.Feedbacks;

namespace LastIsekai
{
    public class Health : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
    {
        public float health;
        float maxHealth;
        public BaseStats stats;
        public PhotonView view;
        public MMFeedbacks hitFeedback;
        public AnimationManager animationManager;
        private float privateHealth;
        private void Awake()
        {
            maxHealth = stats.GetStat(Stat.Health);
            health = maxHealth;
            privateHealth = health;
            view = GetComponent<PhotonView>();
            hitFeedback = GetComponentInChildren<MMFeedbacks>();
            animationManager = GetAnimationManager();

        }


        void Start()
        {
            animationManager = GetAnimationManager();
        }
        void LateUpdate()
        {
            if (view.IsMine)
            {
                if (health != privateHealth)
                {
                    print("I got hit");
                    HitReaction();
                    privateHealth = health;
                }
            }
        }

        private void HitReaction()
        {
            animationManager.animator.SetBool("hit", true);
        }
        public float GetDecimal()
        {
            return health / maxHealth;
        }
        public void HandleDeath()
        {
            print("Dead boih");
        }

        public void TakeDamage(float damage)
        {
            if(hitFeedback != null) hitFeedback.PlayFeedbacks();
            view.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        }
        [PunRPC]
        void RPC_TakeDamage(float damage)
        {
            if (view.IsMine == false) return;
            health -= damage;
            if (health <= 0)
            {
                HandleDeath();
            }
        }
        
        public AnimationManager GetAnimationManager()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    print("I found it");
                    return manager.gameObject.GetComponent<AnimationManager>();
                }
            }
            return null;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(health);
            }
            else if(stream.IsReading)
            {
                health = (float)stream.ReceiveNext();
            }
        }
    }
}