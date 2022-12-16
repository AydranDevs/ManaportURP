using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.Cam
{
    public enum CamZoomState { ZoomingIn, ZoomedIn, ZoomingOut, ZoomedOut }

    public class PartyCam : MonoBehaviour
    {
        public PartyCameraManagerScriptableObject partyCameraManager;

        public CamZoomState camZoomState = CamZoomState.ZoomedOut;

        private const float DEFAULT_CAMERA_SIZE = 7.3125f;

        public Transform target;
        private Vector3 _target;

        Camera cam;

        bool transitionOver = false;

        public float transitionDuration = 2.5f;
        
        void Start() {
            cam = GetComponent<Camera>();
            cam.orthographicSize = DEFAULT_CAMERA_SIZE;

            StartCoroutine(Transition());
        }

        void Update()
        {
            switch (partyCameraManager.GetCameraMode())
            {
                case CameraMode.Hard_Follow: HardFollow_Update(); break;
                case CameraMode.Soft_Follow: SoftFollow_Update(); break;
                case CameraMode.Hard_Move_To_Point: HardMoveToPoint_Update(); break;
                case CameraMode.Soft_Move_To_Point: SoftMoveToPoint_Update(); break;
                case CameraMode.Hard_Cut: HardCut_Update(); break;
                case CameraMode.Soft_Cut: SoftCut_Update(); break;
                default: return;
            }
        }

        private void HardFollow_Update()
        {
            if (partyCameraManager.targets.Count == 0 )
            {
                return;
            }

            transform.position = partyCameraManager.targets[0].position;
        }
        private void SoftFollow_Update()
        {
            if (partyCameraManager.targets.Count == 0)
            {
                return;
            }

            Vector3 slerped = Vector3.Slerp(transform.position, partyCameraManager.targets[0].position, partyCameraManager.cameraSpeed);

            transform.position = slerped;
        }
        private void HardMoveToPoint_Update()
        {
            if (partyCameraManager.point == null)
            {
                return;
            }

            Vector3 lerped = Vector3.Lerp(transform.position, partyCameraManager.point.position, partyCameraManager.cameraSpeed);

            transform.position = lerped;
        }
        private void SoftMoveToPoint_Update()
        {
            if (partyCameraManager.point == null)
            {
                return;
            }

            Vector3 slerped = Vector3.Slerp(transform.position, partyCameraManager.point.position, partyCameraManager.cameraSpeed);

            transform.position = slerped;
        }
        private void HardCut_Update()
        {
            if (partyCameraManager.point == null)
            {
                return; 
            }

            transform.position = partyCameraManager.point.position;
        }
        private void SoftCut_Update()
        {
            if (partyCameraManager.point == null)
            {
                return;
            }

            throw new NotImplementedException();
        }

        // void Update() {
        //     _target = new Vector3(target.position.x, target.position.y, transform.position.z);

        //     if (transitionOver) {
                
                // Vector2 pos = transform.position;
                // Vector2 targetpos = target.position;

                // Vector3 slerped = Vector3.Slerp(pos, targetpos, partyCameraManager.cameraSpeed);

                // slerped.z = transform.position.z;

                // transform.position = slerped;
        //     }

        //     if (camZoomState == CamZoomState.ZoomingIn)
        //         {
        //             Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 2f, 20f * Time.deltaTime);
        //             if (Camera.main.orthographicSize == 2f)
        //             {
        //                 camZoomState = CamZoomState.ZoomedIn;
        //             }
        //         }

        //         if (camZoomState == CamZoomState.ZoomingOut)
        //         {
        //             Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, DEFAULT_CAMERA_SIZE, 20f * Time.deltaTime);
        //             if (Camera.main.orthographicSize == DEFAULT_CAMERA_SIZE)
        //             {
        //                 camZoomState = CamZoomState.ZoomedOut;
        //             }
        //         }
        // }

        public void PartyLeaderChanged() {
            if (target != null) {
                if (transitionOver) {
                    transitionOver = false;
                    StartCoroutine(Transition());
                }
            }
        }

        IEnumerator Transition() {
            float t = 0f;
            Vector3 startPos = transform.position;
            while (t < 1f) {
                t += Time.deltaTime * (Time.timeScale / transitionDuration);
                transform.position = Vector3.Lerp(startPos, _target, t);
                yield return 0;
            }
            transitionOver = true;
        }
    }
}
