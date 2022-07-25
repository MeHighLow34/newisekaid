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
        public CinemachineVirtualCamera camera;
        public AudioListener audioListener;
        private void Awake()
        {
            view = GetComponent<PhotonView>();
            camera = GetComponentInChildren<CinemachineVirtualCamera>();
            audioListener = GetComponentInChildren<AudioListener>();
        }
        private void Start()
        {
            if (!view.IsMine)
            {
                Destroy(camera.gameObject);
                Destroy(audioListener);
            }
        }
    }
}