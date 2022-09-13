using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace LastIsekai
{
    public class AOE : MonoBehaviour
    {
        PhotonView thisPhotonView;
        SphereCollider sphereCollider;
        Collider myCollider;
        public float damage = 15f;
        public float delay = 0.5f;
        public float duration = 3.5f;
        public bool damageEnemies = false;
        [Header("VFX")]
        public GameObject bloodVFX;
        private void Awake()
        {
            myCollider = GetComponent<Collider>();  
            thisPhotonView = GetComponent<PhotonView>();
            if(thisPhotonView == null)
            {
                thisPhotonView = GetComponentInParent<PhotonView>();
            }
            sphereCollider = GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                sphereCollider.enabled = false;
            }
            else
            {
                myCollider.enabled = false;
            }
        }


        private void Update()
        {
            Timer();
            if (damageEnemies)
            {
                if (sphereCollider != null)
                {
                    sphereCollider.enabled = true;
                }
                else
                {
                    myCollider.enabled = true;
                }

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (thisPhotonView.IsMine == false) return;
            var victim = other.GetComponent<IDamageable>();
            if (victim == null) return;
            if (other.gameObject.name == "LocalCharacterDamageDetector")
            {
                print("Hitting myself");

            }
            else
            {
                var damageVictim = other.GetComponent<IDamageable>();
                damageVictim.TakeDamage(damage);
                if (bloodVFX != null)
                {
                    print("Instantiating");
                    Instantiate(bloodVFX, other.gameObject.GetComponent<Collider>().transform.position + new Vector3(0,1,0), Quaternion.identity);
                }
            }
        }
        private void Timer()
        {
            delay -= Time.deltaTime;
            if(delay <= 0) damageEnemies=true;
            if(delay <= -duration)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
