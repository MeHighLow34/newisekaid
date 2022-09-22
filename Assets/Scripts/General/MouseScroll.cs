using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LastIsekai
{
    public class MouseScroll : MonoBehaviour
    {
        CinemachineVirtualCamera cinemachineVirtualCamera;
        public float cameraDistance;
        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            cameraDistance = cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;
        }
        private void Update()
        {
            float zoomChangeAmount = 55f;
            if(Input.mouseScrollDelta.y > 0)
            {
                cameraDistance -= zoomChangeAmount * Time.deltaTime * 10f;
                cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = cameraDistance;
            }
            if(Input.mouseScrollDelta.y < 0)
            {
               cameraDistance += zoomChangeAmount * Time.deltaTime * 10f;
                cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = cameraDistance;
            }
            if (cameraDistance <= 5)
            {
                cameraDistance = 5;
                cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = cameraDistance;
            }
            if (cameraDistance >= 15)
            {
                cameraDistance = 15;
                cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = cameraDistance;
            }
        }
    }
}
