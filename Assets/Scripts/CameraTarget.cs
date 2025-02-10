using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField]
        Camera camera;

        [SerializeField]
        Transform player;

        [SerializeField]
        float threshold;

      

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = (player.position + mousePos)/2f;

            targetPos.x = Mathf.Clamp(targetPos.x,-threshold+player.position.x,threshold+ player.position.x);
            targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

            this.transform.position = targetPos;
        }
    }

}
