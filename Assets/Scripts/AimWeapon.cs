using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class AimWeapon : MonoBehaviour
    {
        private Transform aimTrabform;

        private void Awake()
        {
            aimTrabform = transform.Find("Aim");
        }

        private void Update()
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            aimTrabform.LookAt(mousePosition);
        }


        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            return new Vector3();
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }

   

}
