using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class EnemyWeaponDetector : MonoBehaviour
    {
        public float damage = 25f;
        public EnemyHealth myHealth;
        private Collider weaponCollider;

        private void Awake()
        {
            weaponCollider = GetComponent<Collider>();
            weaponCollider.enabled = false; 
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable victim = other.gameObject.GetComponent<IDamageable>();
            print(other.gameObject.name + " goblincik je udario");
            if (victim != null && myHealth != victim)
            {
                victim.TakeDamage(damage);
            }
        }
    }
}