using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

namespace LastIsekai
{
    public class EnemyHealth :  MonoBehaviour, IDamageable, IPunObservable
    {
        [Header("Dependencies")]
        public EnemyAnimatorManager enemyAnimatorManager;
        public EnemyAnimationEvents enemyAnimationEvents;
        public BaseStats baseStats;
        public EnemyManager enemyManager;
        public Ragdoll ragdoll;
        public GameObject worldUI;
        [Header("Properties")]
        public float health;
        float maxHealth;
        [Header("Transform")]
        public Transform effectInstantionPoint;

        void Awake()
        {
            maxHealth = baseStats.GetStat(Stat.Health);
            health = maxHealth;
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            HitReaction();
            if(health <= 0)
            {
                HandleDeath();
            }

        }

        public void HitReaction()
        {
            enemyAnimationEvents.DisableWeapon();
            enemyAnimatorManager.PlayAnimation("Hit", true);
            PhotonNetwork.Instantiate("BloodVFX", effectInstantionPoint.position, Quaternion.identity);
            PhotonNetwork.Instantiate("Spark", effectInstantionPoint.position, Quaternion.identity);
            enemyManager.currentRecoveryTime = 1.25f;
        }

        private void HandleDeath()
        {
            enemyAnimationEvents.DisableWeapon();
            ragdoll.EnableRagdoll();
            enemyManager.enabled = false;
            enemyManager.enemyLocomotionManager.navMeshAgent.enabled = false;
            worldUI.SetActive(false);
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(health);
            }
            else if (stream.IsReading)
            {
                health = (float)stream.ReceiveNext();
            }
        }

        public float GetDecimal()
        {
            return health / maxHealth;
        }
    }
}
