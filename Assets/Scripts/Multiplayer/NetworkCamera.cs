using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

namespace LastIsekai
{
    public class NetworkCamera : MonoBehaviour
    {
        public PhotonView view;
        [Header("Destroying/Disabling Objects")]
        public CinemachineVirtualCamera camera;
        public AudioListener audioListener;
        public GameObject worldSpaceUI;
        public Camera mainCamera;
        public GameObject itemInfo;
        public GameObject removeWarning;
        public Hotbar hotbar;
        public HotbarUI hotbarUI;
        public GameObject damageDetector;
        private void Awake()
        {
            view = GetComponent<PhotonView>();
            camera = GetComponentInChildren<CinemachineVirtualCamera>();
            audioListener = GetComponentInChildren<AudioListener>();
            #region Destruction
            if (view.IsMine)
            {
                damageDetector.gameObject.name = "LocalCharacterDamageDetector";
                Destroy(worldSpaceUI);
            }
            if(view.IsMine == false)
            {
                Destroy(mainCamera.gameObject);
                Destroy(itemInfo);
                Destroy(removeWarning);
                Destroy(hotbar);
                Destroy(hotbarUI);
            }
        }


        private void Start()
        {
            if (!view.IsMine)
            {
                Destroy(camera.gameObject);
                Destroy(audioListener);
            }
        }
        #endregion
    }
}