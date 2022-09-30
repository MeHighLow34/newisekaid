using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class HoldDamage : MonoBehaviour
    {
        [Header("Props")]
        public float damage = 5f;
        public float delayTime = 3f;
        public bool canAttack;
        float passedTime = 0;
        PhotonView photonView;
        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
            canAttack = true;
        }


        private void OnTriggerStay(Collider other)
        {
            if (canAttack)
            {
                if (photonView.IsMine == false) return;
                var victim = other.GetComponent<IDamageable>();
                if (victim == null) return;
                if (other.gameObject.name == "LocalCharacterDamageDetector")
                {
                    print("Hitting myself");

                }
                else
                {
                    var enemyPV = other.GetComponent<PhotonView>();
                    var enemyHealth = other.GetComponent<Mediary>().healther;
                    enemyHealth.ChangeHitAnimation(99, enemyPV.ViewID);
                    var damageVictim = other.GetComponent<IDamageable>();
                    damageVictim.TakeDamage(damage);
                    /*                if (bloodVFX != null)
                                    {
                                        print("Instantiating");
                                        Instantiate(bloodVFX, other.gameObject.GetComponent<Collider>().transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                                    }*/
                }
                canAttack = false;
            }
        }

        private void Update() 
        {
          //  damage += Time.deltaTime;
            passedTime += Time.deltaTime;
            if(passedTime > delayTime)
            {
                canAttack = true;
                passedTime = 0;
            }
        }
    }
}