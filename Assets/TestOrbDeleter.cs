using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class TestOrbDeleter : MonoBehaviour
    {
        Transform direction;
        public float speed = 7.45f;
        Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            direction = transform;
        }

        private void Update()
        {
            rb.AddRelativeForce(direction.forward * speed * 10 * Time.deltaTime);
        }

        private Transform GetInstantiationTransform()
        {
            WeaponManager[] allWeaponManagers = FindObjectsOfType<WeaponManager>();
            foreach (var manager in allWeaponManagers)
            {
                if (manager.gameObject.name == "LocalPlayerStructure")
                {
                    return manager.transform;
                }
            }
            return null;
        }

    }
}
