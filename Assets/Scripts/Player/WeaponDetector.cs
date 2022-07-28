using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class WeaponDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var victim = other.GetComponent<IDamageable>();
            if (victim == null) return;
            print("I hit that bitch " + victim);
        }
    }
}
