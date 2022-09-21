using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LastIsekai
{
    public class MouseScroll : MonoBehaviour
    {
        public float cameraDistance;
        private void Awake()
        {
           // mainCamera = GetComponent<CinemachineVirtualCamera>();
            //   var mameraDistance = mainCamera.GetCinemachineComponent<3rdPersonFollow>(); 
            cameraDistance = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;
        }
        private void Update()
        {
            float zoomChangeAmount = 55f;
            if(Input.mouseScrollDelta.y > 0)
            {
              cameraDistance -= zoomChangeAmount * Time.deltaTime * 10f;
            }
            if(Input.mouseScrollDelta.y < 0)
            {
                cameraDistance += zoomChangeAmount * Time.deltaTime * 10f;

            }
            if(cameraDistance <= 5)
            {
                cameraDistance = 5;
            }
            if(cameraDistance >= 10)
            {
                cameraDistance = 10; 
            }
        }
    }
}
