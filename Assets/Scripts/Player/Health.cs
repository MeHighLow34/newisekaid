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
        private void Awake()
        {
            maxHealth = stats.GetStat(Stat.Health);
            health = maxHealth;
            view = GetComponent<PhotonView>();
            hitFeedback = GetComponentInChildren<MMFeedbacks>();
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
            if(health <= 0)
            {
                HandleDeath();
            }
        }
        
        private PhotonView FindPhotonView()
        {
            WeaponManager weaponManager;
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    weaponManager = manager;
                    return weaponManager.gameObject.GetComponentInParent<PhotonView>();
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