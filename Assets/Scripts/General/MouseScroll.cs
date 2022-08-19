using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace LastIsekai
{
    public class MouseScroll : MonoBehaviour
    {
        CinemachineVirtualCamera mainCamera;

        private void Awake()
        {
            mainCamera = GetComponent<CinemachineVirtualCamera>();
        }
        private void Update()
        {
            float zoomChangeAmount = 80f;
            if(Input.mouseScrollDelta.y > 0)
            {
                mainCamera.m_Lens.FieldOfView -= zoomChangeAmount * Time.deltaTime * 10f;
            }
            if(Input.mouseScrollDelta.y < 0)
            {
                mainCamera.m_Lens.FieldOfView += zoomChangeAmount * Time.deltaTime * 10f;

            }
            if(mainCamera.m_Lens.FieldOfView <= 55)
            {
                mainCamera.m_Lens.FieldOfView = 55;
            }
            if(mainCamera.m_Lens.FieldOfView >= 105)
            {
                mainCamera.m_Lens.FieldOfView = 105;
            }
        }
    }
}
