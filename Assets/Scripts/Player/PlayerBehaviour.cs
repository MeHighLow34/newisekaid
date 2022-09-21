using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using StarterAssets;
using Photon.Pun;

namespace LastIsekai
{
    public class PlayerBehaviour : MonoBehaviour
    {
        PhotonView myLocalPhotonView;
        ThirdPersonController thirdPersonController;
        InputManager inputManager;
        PlayerManager playerManager;
        AnimationManager animationManager;
        [SerializeField] float defaultMove;
        [SerializeField] float defaultSprint;
        [Header("Aim - Properties")]
        public GameObject mainCamera;
        public GameObject aimCamera;
        public GameObject crosshair;
        [Header("Rage - Properties")]
        public Volume rageVisuals;
        public VisualEffect rageVFX;
        public float maxRageDuration = 5f;
        public float rageDuration = 5f;
        public bool rageEnabled;
        [SerializeField] float rageMove;
        [SerializeField] float rageSprint;
        [Header("Beam - Properties")]
        public bool beamEnabled;
        public float beamDuration;
        private float beamTimeElapsed;
        private void Awake()
        {
            myLocalPhotonView = GetComponentInParent<PhotonView>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            inputManager = GetComponent<InputManager>();
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            rageVisuals = GameObject.FindGameObjectWithTag("RageVISUAL").GetComponent<Volume>();
            rageVFX = GameObject.FindGameObjectWithTag("RageVFX").GetComponent<VisualEffect>();
            playerManager = GetComponent<PlayerManager>();
            animationManager = GetComponent<AnimationManager>();
        }

        private void Update()
        {
            if (beamEnabled)
            {
                EnableAim();
                beamTimeElapsed += Time.deltaTime;
                if(beamTimeElapsed >= beamDuration)
                {
                    beamTimeElapsed = 0f;
                    DisableBeam();
                }
            }
            #region Rage
            if (rageEnabled)
            {
                rageDuration -= Time.deltaTime;
                if(rageDuration <= 0)
                {
                    DisableRage();
                    rageDuration = maxRageDuration;
                    rageEnabled = false;
                }
            }
            #endregion
            #region Aiming
            if (playerManager.playerClass == PlayerClass.Mage && !beamEnabled)
            {
                if (inputManager.aimFlag)
                {
                    EnableAim();
                }
                else
                {
                    DisableAim();
                }
            }
            #endregion
            #region Blocking
            else if (playerManager.playerClass == PlayerClass.Tank)
            {
                if (inputManager.aimFlag)
                {
                    animationManager.animator.SetBool("block", true);
                    animationManager.animator.SetBool("isInteracting", true);
                }
                else
                {
               //     animationManager.animator.SetBool("block", false);
                }
            }
            #endregion
        }

        private void DisableAim()
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            crosshair.SetActive(false);
        }

        private void EnableAim()
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            crosshair.SetActive(true);
        }

        public void RageEnabled()
        {
                rageEnabled = true;
                rageVisuals.weight = 1f;
                thirdPersonController.MoveSpeed = rageMove;
                thirdPersonController.SprintSpeed = rageSprint;
                rageVFX.enabled = true;       
        }

        private void DisableRage()
        {
            rageVFX.enabled = false;
            thirdPersonController.MoveSpeed = defaultMove;
            thirdPersonController.SprintSpeed = defaultSprint;  
            rageVisuals.weight = 0f;
        }

        public void BeamEnabled()
        {
            beamEnabled = true;
        }

        public void DisableBeam()
        {
            beamEnabled = false;
            animationManager.animator.SetBool("beam", false);
            DisableAim();
        }

    }
}
