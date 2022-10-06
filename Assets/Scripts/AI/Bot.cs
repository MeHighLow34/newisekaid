using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LastIsekai
{
    public class Bot : MonoBehaviour
    {
        public NavMeshAgent agent;
        public bool startChase;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var player = GameObject.FindGameObjectWithTag("PlayerBody");
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 10) startChase = true;
            if (startChase)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }
}
