using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class FootInverseKinematics : MonoBehaviour
    {
        Animator animator;
        [Header("Properties")]
        public csHomebrewIK footIK;
        public float feelSpeed = 10f;
        public float idle_walk_threshold = 2;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            footIK = GetComponent<csHomebrewIK>();  
        }

        private void Update()
        {
            float speed = animator.GetFloat("Speed");
            
            if(speed <= idle_walk_threshold)
            {
                footIK.globalWeight = Mathf.Lerp(footIK.globalWeight, 1, feelSpeed * Time.deltaTime);
            }
            else if(footIK.globalWeight >= 0.1)
            {
                footIK.globalWeight = Mathf.Lerp(footIK.globalWeight, 0, feelSpeed * Time.deltaTime);
            }
        }


    }
}
