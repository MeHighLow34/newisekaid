using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class Tornado : MonoBehaviour
    {
        public float speed = 15f;
        public Vector3 playerDirection;


        private void Start()
        {
            var player = GameObject.Find("LocalBody");
            playerDirection = player.transform.forward;
        }

        private void Update()
        {
            transform.position += playerDirection * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}