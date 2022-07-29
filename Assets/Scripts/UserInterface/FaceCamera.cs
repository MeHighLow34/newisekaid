using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LastIsekai
{
    public class FaceCamera : MonoBehaviour
    {
        public Camera camera;



        private void Start()
        {
            camera = Camera.main;   
        }


        private void Update()
        {
            transform.forward = camera.transform.forward;
        }

    }
}