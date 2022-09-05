using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastIsekai
{
    public class PlayerManager : MonoBehaviour
    {
        public Animator animator;
        InputManager inputManager;
        [Header("Bools")]
        public bool isInteracting;
        public bool canDoCombo;
        public bool noInteracting;
        public bool attack;
        public bool block;
        public bool stepBack;
        [Header("Stats")]
        public PlayerClass playerClass;
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            inputManager.HandleAllInput(); 
        }

        private void LateUpdate()
        {
            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");
            noInteracting = animator.GetBool("noInteracting");
            attack = animator.GetBool("attack");
            block = animator.GetBool("block");
            stepBack = animator.GetBool("stepBack");
        }
    }
}
