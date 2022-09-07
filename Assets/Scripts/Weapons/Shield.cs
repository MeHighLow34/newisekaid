using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Shield : MonoBehaviour
    {
        public Health myHealth;
        PhotonView photonView;
        public AnimationManager animationManager;
        public PlayerManager playerManager;
        public bool blocking;
        public bool abilityShield;

        private void Awake()
        {
            myHealth = GetComponentInParent<WeaponIntermediary>().myHealth;
        }

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            playerManager = GetComponentInParent<WeaponIntermediary>().playerManager;
            animationManager = playerManager.gameObject.GetComponent<AnimationManager>();
        }
        public void BlockingReaction()
        {
            myHealth.TakeDamage(0.5f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            var weapon = collision.gameObject.GetComponent<WeaponDetector>();
            if(weapon != null)
            {
                BlockReaction();
            }
        }

        private void Update()
        {
            if (abilityShield)
            {
                blocking = true;
            }
            else
            {
                blocking = playerManager.block;
            }
        }

        public void BlockReaction()
        {
            animationManager.animator.SetBool("blockImpact", true);
        }
    }
}
