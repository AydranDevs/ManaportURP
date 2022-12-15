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

        public class CameraState
        {
            /// <summary>
            /// the camera will find a center point between these transforms if targets.Length > 0
            /// </summary>
            public Transform[] targets;
        }
        private CameraState _cameraState;

        public CamZoomState camZoomState = CamZoomState.ZoomedOut;

        private const float DEFAULT_CAMERA_SIZE = 7.3125f;

        public float speed = 1f;
        public Transform target;
        private Vector3 _target;

        Camera cam;

        bool transitionOver = false;

        public float transitionDuration = 2.5f;

        public void SetCameraState(CameraState cameraState)
        {
            _cameraState = cameraState;
        }
        
        void Start() {
            cam = GetComponent<Camera>();
            cam.orthographicSize = DEFAULT_CAMERA_SIZE;

            StartCoroutine(Transition());
        }

        void Update() {
            _target = new Vector3(target.position.x, target.position.y, transform.position.z);

            if (transitionOver) {
                
                Vector2 pos = transform.position;
                Vector2 targetpos = target.position;

                Vector3 slerped = Vector3.Slerp(pos, targetpos, speed);

                slerped.z = transform.position.z;

                transform.position = slerped;
            }

            if (camZoomState == CamZoomState.ZoomingIn)
                {
                    Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, 2f, 20f * Time.deltaTime);
                    if (Camera.main.orthographicSize == 2f)
                    {
                        camZoomState = CamZoomState.ZoomedIn;
                    }
                }

                if (camZoomState == CamZoomState.ZoomingOut)
                {
                    Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, DEFAULT_CAMERA_SIZE, 20f * Time.deltaTime);
                    if (Camera.main.orthographicSize == DEFAULT_CAMERA_SIZE)
                    {
                        camZoomState = CamZoomState.ZoomedOut;
                    }
                }
        }

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
