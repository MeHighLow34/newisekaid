using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace LastIsekai
{
    public class Orb : MonoBehaviour
    {
        Vector3 direction;
        public float speed = 7.45f;
        Rigidbody rb;
        Camera cam;
        [Header("Properties")]
        public float minViewable = 45f;
        public float maxViewable = 75f;
        public GameObject joke;
        private void Awake()
        {
            cam = Camera.main;
            rb = GetComponent<Rigidbody>();
        }


        private void Start()
        {
            Vector3 targetDirection;
            Transform player = GetInstantiationTransform();
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetDirection = hit.point;
            }
            else
            {
                targetDirection = cam.transform.forward;
            }
            float viewAbleAngle = Vector3.Angle(targetDirection, player.forward);
            if (viewAbleAngle >= minViewable && viewAbleAngle <= maxViewable)
            {
                direction = cam.transform.forward;
            }
            else
            {
                direction = player.forward;
            }
        }

        private void Update()
        {
            rb.AddRelativeForce(direction * speed * 10 * Time.deltaTime);
        }


        private void OnCollisionEnter(Collision collision)
        {
            print(collision);
            /*PhotonNetwork.Instantiate("electroAbilityVFX", collision.contacts[0].point + new Vector3(0, 3.5f, 0), Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);*/
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
