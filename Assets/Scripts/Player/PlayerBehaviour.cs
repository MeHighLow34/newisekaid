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
        [Header("TankSword - Properties")]
        public bool tankSwordEnabled;
        public Weapon tankSwordWeapon;
        public Weapon defaultWeapon;
        public WeaponManager weaponManager;
        public UIManager uIManager;
        public float tankSwordDuration;
        private float tankSwordTimeElapsed;
        private void Awake()
        {
            myLocalPhotonView = GetComponentInParent<PhotonView>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            inputManager = GetComponent<InputManager>();
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            rageVisuals = GameObject.FindGameObjectWithTag("RageVISUAL").GetComponent<Volume>();
           // rageVFX = GameObject.FindGameObjectWithTag("RageVFX").GetComponent<VisualEffect>();
            playerManager = GetComponent<PlayerManager>();
            animationManager = GetComponent<AnimationManager>();
            weaponManager = GetComponent<WeaponManager>();
            uIManager = FindObjectOfType<UIManager>();
        }

        private void Update()
        {

            if(tankSwordEnabled)
            {
                beamEnabled = false;
                //  uIManager.inventoryEnabled = false;
                tankSwordTimeElapsed += Time.deltaTime;
                if(tankSwordTimeElapsed > tankSwordDuration)
                {
                    tankSwordTimeElapsed = 0f;
                    DisableTankSword(); 
                }
            }

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
                    animationManager.animator.SetBool("block", false);
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
            thirdPersonController.MoveSpeed = defaultMove;
            thirdPersonController.SprintSpeed = defaultSprint;  
            rageVisuals.weight = 0f;
            rageVFX.enabled = false;
        }

        public void TankSwordEnabled()
        {
            tankSwordEnabled = true;
            beamEnabled = false;
            weaponManager.EquipWeapon(tankSwordWeapon);
            uIManager.inventoryEnabled = false;
            uIManager.inventoryShutDown = true;
        }

        public void DisableTankSword()
        {
            tankSwordEnabled = false;
            weaponManager.EquipWeapon(defaultWeapon);
            Inventory.instance.Remove(tankSwordWeapon);
            Inventory.instance.Remove(defaultWeapon);
            uIManager.inventoryShutDown = false;
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
