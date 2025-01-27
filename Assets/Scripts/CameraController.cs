using Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace Game
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float distance = 5f;
        public float hight = 2f;
        public float rotationSpeed = 5f;
        public float zoomSpeed = 2f;
        public float minZoom = 2f; 
        public float maxZoom = 10f;
        public float heightSmooth = 0.5f; 
        public float rotationSmooth = 0.1f;

        private float currentRotation = 0f;
        private float currentHeight = 0f;
        
        void Start ()
        {
            if(target == null)
            {
                target = Camera.main.transform;
            }

            currentHeight = hight;
            currentRotation = transform.eulerAngles.y;
        }

        void Update ()
        {
            HandleRotationInput();
            HandleZoomInput();
        }

        void LateUpdate()
        {
            FollowTarget();
            SmoothTransition();
        }

        void HandleRotationInput()
        {
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            currentRotation += horizontal * rotationSpeed;
            currentHeight += vertical +1;

            currentHeight = Mathf.Clamp(currentHeight, 0f, 10f);

        }

        void HandleZoomInput()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll != 0f)
            {
                distance -= scroll* zoomSpeed;
                distance = Mathf.Clamp(distance,minZoom,maxZoom);
            }
        }

        void FollowTarget()
        {
            Vector3 targetPosition = target.position;
            targetPosition.y = currentHeight;

            Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);
            Vector3 direction = new Vector3(0,0,-distance);
            Vector3 cameraPosition = targetPosition + rotation * direction;

            transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.05f);
        }

        void SmoothTransition()
        {
            Quaternion targetPosition = Quaternion.Euler(0f,currentRotation, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPosition, rotationSmooth);
        }
    }
}

