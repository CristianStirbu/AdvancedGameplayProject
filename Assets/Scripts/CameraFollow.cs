using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        private float currentRotation = 0f;
        private float currentHeight = 0f;
        public float distance = 5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void LateUpdate()
        {
            FollowTarget();
        }

        void FollowTarget()
        {
            Vector3 targetPosition = target.position;
            //targetPosition.y = currentHeight;

           // Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);
            Vector3 direction = new Vector3(0, 10, 0);
            Vector3 cameraPosition = targetPosition + direction;

            transform.position = Vector3.Lerp(transform.position, cameraPosition, 0.05f);
        }
    }

}
