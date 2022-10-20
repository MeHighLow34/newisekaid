using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class EnemyWeaponDetector : MonoBehaviour
    {
        public float damage = 25f;
        public EnemyHealth myHealth;
        public Animator animator;
        private Collider weaponCollider;
        public PhotonView photonView;

        private void Awake()
        {
            weaponCollider = GetComponent<Collider>();
            weaponCollider.enabled = false;
            photonView = GetComponent<PhotonView>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine == false) return;
            IDamageable victim = other.gameObject.GetComponent<IDamageable>();
            print(other.gameObject.name + " goblincik je udario");
            if (victim != null && myHealth != victim && other.gameObject.tag != "EnemyHealth")
            {
                victim.TakeDamage(damage);
            }
        }
    }
}