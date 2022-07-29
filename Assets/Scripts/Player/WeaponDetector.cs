using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class WeaponDetector : MonoBehaviour
    {
        public float damage = 3.5f;
        private void OnTriggerEnter(Collider other)
        {
            var victim = other.GetComponent<IDamageable>();
            if (victim == null) return;
            victim.TakeDamage(damage);
        }
    }
}
