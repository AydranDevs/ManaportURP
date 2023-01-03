using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.PartySystem.Cam
{
    public enum CamZoomState { ZoomingIn, ZoomedIn, ZoomingOut, ZoomedOut }

    public class PartyCam : MonoBehaviour
    {
        public PartyCameraManagerScriptableObject partyCameraManager;

        // public CamZoomState camZoomState = CamZoomState.ZoomedOut;

        private const float SPEED_UP_TO_MEET_TARGET_THRESHOLD = 100f;
        private const float DEFAULT_MIN_CAMERA_SIZE = 7.3125f;

        Camera cam;

        private Vector2 _targetPosition;

        private Vector2 _maxTargetPosition;
        private Vector2 _minTargetPosition;

        void Awake() {
            partyCameraManager.SetPartyCam(this);    
            cam = GetComponent<Camera>();
            cam.orthographicSize = DEFAULT_MIN_CAMERA_SIZE;
        }

        void CalculateTargetPosition()
        {
            float xSum = 0f;
            float ySum = 0f;
            float xAvg = 0f;
            float yAvg = 0f;
            for (int i = 0; i < partyCameraManager.targets.Count; i++)
            {
                xSum += partyCameraManager.targets[i].position.x;
                ySum += partyCameraManager.targets[i].position.y;
            }
            xAvg = xSum / partyCameraManager.targets.Count;
            yAvg = ySum / partyCameraManager.targets.Count;
            _targetPosition = new Vector2(xAvg, yAvg);
        }

        void CalculateBoundingBox()
        {
            float minX = partyCameraManager.targets[0].position.x;
            float minY = partyCameraManager.targets[0].position.y;
            float maxX = partyCameraManager.targets[0].position.x;
            float maxY = partyCameraManager.targets[0].position.y;
            
            for (int i = 0; i < partyCameraManager.targets.Count; i++)
            {
                minX = Mathf.Min(partyCameraManager.targets[i].position.x, minX);
                minY = Mathf.Min(partyCameraManager.targets[i].position.y, minY);
                maxX = Mathf.Max(partyCameraManager.targets[i].position.x, maxX);
                maxY = Mathf.Max(partyCameraManager.targets[i].position.y, maxY);
            }

            _maxTargetPosition = new Vector2(maxX, maxY);
            _minTargetPosition = new Vector2(minX, minY);
        }

        void CalculateOrthoSize()
        {
            var dist = Vector2.Distance(_minTargetPosition, _maxTargetPosition) / 2.5f;

            var lerped = Vector2.Lerp(
                new Vector2(cam.orthographicSize, 0f),
                new Vector2(dist, 0f),
                partyCameraManager.cameraSpeed
                );

            if (dist > DEFAULT_MIN_CAMERA_SIZE)
            {
                cam.orthographicSize = lerped.x;                    
            }
            else
            {
                lerped = Vector2.Lerp(
                    new Vector2(cam.orthographicSize, 0f),
                    new Vector2(DEFAULT_MIN_CAMERA_SIZE, 0f),
                    partyCameraManager.cameraSpeed
                    );

                cam.orthographicSize = lerped.x;    
            }
        }

        void Update()
        {   
            CalculateTargetPosition();
            CalculateBoundingBox();
            CalculateOrthoSize();

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

            transform.position = new Vector3(
                _targetPosition.x,
                _targetPosition.y,
                -1f
            );
        }
        private void SoftFollow_Update()
        {
            if (partyCameraManager.targets.Count == 0)
            {
                return;
            }

            var sp = partyCameraManager.cameraSpeed;
            var targetDist = Vector2.Distance(transform.position, _targetPosition);
            if (targetDist >= SPEED_UP_TO_MEET_TARGET_THRESHOLD)
            {
                sp = partyCameraManager.cameraSpeed * 5;
            }
            Vector3 slerped = Vector3.Slerp(transform.position, _targetPosition, sp);

            transform.position = new Vector3(
                slerped.x,
                slerped.y,
                -1f
            );
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

        private void OnDrawGizmos() {
            Gizmos.DrawLine(transform.position, _targetPosition);
            Gizmos.DrawLine(_minTargetPosition, _maxTargetPosition);
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
    }
}
