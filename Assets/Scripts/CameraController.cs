using Events;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : EventHandler
    {
        public abstract class CameraEvent: GameEvent
        {
            private CameraController C_controller;

            public CameraEvent(CameraController controller)
            {
                C_controller = controller;
            }

            public override bool IsDone()
            {
                return false;
            }

        }

        [SerializeField, Range(0.0f, 1.0f)]
        public float Angle = 0.0f;

        [SerializeField, Range(1.0f, 10.0f)]
        public float Distance = 4.0f;

        [SerializeField, Range(1, 10)]
        public int SmoothingIterations = 1;

        public const float MaxAngle = 70.0f;

        private Camera camera;
        private int LevelMask;
        private static CameraController instance;

        private void OnEnable()
        {
            camera = GetComponent<Camera>();
            LevelMask = LayerMask.GetMask(new string[] { "level" });
            //PushEvent(new ExplorationCameraEvent(this));
            instance = this;
        }

        private void OnDisable()
        {
            instance = (instance == this ? null : instance);
        }

    }
}

