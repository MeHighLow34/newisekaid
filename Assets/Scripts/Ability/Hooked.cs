using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class Hooked : MonoBehaviour
    {

        public bool hooked;
        public GameObject playerBody;
        public Vector3 destination;
        public float stopRange;
        public float speed;
        AnimationManager animationManager;
        PlayerBehaviour playerBehaviour;
        public CharacterController characterController;

        private void Awake()
        {
            playerBehaviour = GetComponent<PlayerBehaviour>();
            animationManager = GetComponent<AnimationManager>();
        }
        private void Update()
        {
            if (hooked)
            {
                playerBehaviour.DisableBeam();
                characterController.enabled = false;
                animationManager.animator.SetBool("hooked", true);
                float distance = Vector3.Distance(playerBody.transform.position, destination);
                Vector3 finalPosition = new Vector3(destination.x, playerBody.transform.position.y, destination.z);
                Vector3 directionToMove = finalPosition - playerBody.transform.position;
                directionToMove = directionToMove.normalized * Time.deltaTime * speed;
                playerBody.transform.position = playerBody.transform.position + Vector3.ClampMagnitude(directionToMove, stopRange);
                if(distance <= stopRange)
                {
                    characterController.enabled = true;
                    animationManager.animator.SetBool("hooked", false);
                    hooked = false;
                }
            }
        }

        public void GotHooked(Vector3 destinationHook, float hookStopRange, float hookSpeed)
        {
            speed = hookSpeed;
            stopRange = hookStopRange;  
            destination = destinationHook;
            hooked = true;
        }
    }
}