using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

namespace LastIsekai
{
    public class FootInverseKinematics : MonoBehaviour
    {
        Animator animator;
        [Header("Properties")]
        public ThirdPersonController controller;
        public csHomebrewIK footIK;
        public float smoothSpeed = 0.075f;
        public float feelSpeed = 10f;
        public float idle_walk_threshold = 2;
        public bool isInteracting;
        public bool grounded;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            footIK = GetComponent<csHomebrewIK>();  
        }

        private void Update()
        {
            float speed = animator.GetFloat("Speed");
            isInteracting = animator.GetBool("isInteracting");
            grounded = animator.GetBool("Grounded");
            if (isInteracting == false && grounded == true) // Only when we are doing locomotion
            {
                if (speed <= idle_walk_threshold)
                {
                    footIK.globalWeight = Mathf.Lerp(footIK.globalWeight, 1, feelSpeed * Time.deltaTime);
                    footIK.smoothTime = smoothSpeed;
                    // footIK.enableBodyPositioning = false;

                    if (footIK.crouchRange != 0.85f)
                    {
                        footIK.crouchRange = Mathf.Lerp(footIK.crouchRange, 0.85f, feelSpeed * Time.deltaTime);
                        if (footIK.crouchRange == 0.85f)
                        {
                            footIK.enableBodyPositioning = false;
                        }
                    }
                    controller.GroundedOffset = 0.1f;
                    controller.GroundedRadius = 0.1f;
                }
                else if (footIK.globalWeight >= 0.1)
                {
                    footIK.globalWeight = Mathf.Lerp(footIK.globalWeight, 0, feelSpeed * Time.deltaTime);
                    footIK.smoothTime = smoothSpeed;
                    footIK.enableBodyPositioning = false;
                    if (footIK.crouchRange != 0.85f)
                    {
                        footIK.crouchRange = Mathf.Lerp(footIK.crouchRange, 0.85f, feelSpeed * Time.deltaTime);
                        if (footIK.crouchRange == 0.85f)
                        {
                            footIK.enableBodyPositioning = false;
                        }
                    }
                    controller.GroundedOffset = 0.1f;
                    controller.GroundedRadius = 0.1f;
                }
            }
            if(grounded == false && isInteracting == false) // Only when we are falling 
            {
                footIK.globalWeight = Mathf.Lerp(footIK.globalWeight, 0, feelSpeed * Time.deltaTime); 
            }

            // FootIK for isInteracting animations decides the animation event FootIK
        }


    }
}
