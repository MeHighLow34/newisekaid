using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class PlayerBehaviour : MonoBehaviour
    {
        InputManager inputManager;
        [Header("Aim - Properties")]
        public GameObject mainCamera;
        public GameObject aimCamera;
        public GameObject crosshair;
        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        }

        private void Update()
        {
            if (inputManager.aimFlag)
            {
                mainCamera.SetActive(false);
                aimCamera.SetActive(true);
                crosshair.SetActive(true);
            }
            else
            {
                mainCamera.SetActive(true);
                aimCamera.SetActive(false);
                crosshair.SetActive(false);
            }
        }

    }
}
