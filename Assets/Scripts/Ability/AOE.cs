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
        public float damage = 15f;
        public float delay = 0.5f;
        public bool damageEnemies = false;
        [Header("VFX")]
        public GameObject bloodVFX;
        private void Awake()
        {
            thisPhotonView = GetComponent<PhotonView>();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
        }


        private void Update()
        {
            Timer();
            if (damageEnemies)
            {
                sphereCollider.enabled = true;
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
            if(delay <= -2.5f)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
