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
        private void Awake()
        {
            view = GetComponent<PhotonView>();
            camera = GetComponentInChildren<CinemachineVirtualCamera>();
            audioListener = GetComponentInChildren<AudioListener>();
            #region Destruction
            if (view.IsMine)
            {
                Destroy(worldSpaceUI);
            }
            if(view.IsMine == false)
            {
                Destroy(mainCamera.gameObject);
                Destroy(itemInfo);
                Destroy(removeWarning);
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