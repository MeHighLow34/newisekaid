using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class WeaponDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            print(other.gameObject.name + " I HIT THAT ");
        }
    }
}
