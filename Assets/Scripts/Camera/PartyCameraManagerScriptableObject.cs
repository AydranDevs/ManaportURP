using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Manapotion.PartySystem.Cam
{
    public enum CameraMode
    {
        Hard_Follow,
        Soft_Follow, 
        Hard_Move_To_Point,
        Soft_Move_To_Point,
        Hard_Cut,
        Soft_Cut,
    }

    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/New PartyCameraManagerScriptableObject")]
    public class PartyCameraManagerScriptableObject : ScriptableObject
    {
        private CameraMode _cameraMode; 

        public event Action CameraTargetAdded;
        public event Action CameraTargetRemoved;
        public event Action<Transform> CameraTargetBiasChanged;

        [NonSerialized]
        public List<Transform> targets;

        public Transform point;
        private bool _canAddTarget = true;
        private bool _canRemoveTarget = true;

        private PartyCam _partyCamera;

        public float cameraSpeed;

        // returns true if this camera is allowed to have another target added
        private bool RequestAddTarget(Transform target)
        {
            if (targets == null)
            {
                targets = new List<Transform>();
            }
            if (!_canAddTarget)
            {
                return false;
            }
            else if (targets.Contains(target))
            {
                return false;
            }

            return true;
        }
        // returns true if this camera is allowed to have a target removed
        private bool RequestRemoveTarget(Transform target)
        {
            if (targets == null)
            {
                targets = new List<Transform>();
            }
            if (!_canRemoveTarget)
            {
                return false;
            }
            if (!targets.Contains(target))
            {
                return false;
            }

            return true;
        }

        public IEnumerator AddCameraTarget(Transform target)
        {
            if (!RequestAddTarget(target))
            {
                yield break;
            }

            targets.Add(target);
            CameraTargetAdded?.Invoke();
            yield break;
        }

        public IEnumerator RemoveCameraTarget(Transform target)
        {
            if (!RequestRemoveTarget(target))
            {
                yield break;
            }

            targets.Remove(target);
            CameraTargetRemoved?.Invoke();
            yield break;
        }

        public IEnumerator SetCameraTargets(List<Transform> targets)
        {
            this.targets = targets;
            yield break;
        }

        public IEnumerator SetCameraTargetBias(Transform biasedTarget = null, int biasedTargetIndex = 0)
        {
            // first check if the target transform is a part of the target list of if the target index is within the target list bounds
            // then make the camera follow the biased target more strongly than the rest

            throw new NotImplementedException();
        }

        public void GetCameraPosition(out Vector3 cameraPosition)
        {
            cameraPosition = _partyCamera.transform.position;
        }

        public PartyCam GetPartyCam()
        {
            return _partyCamera;
        }

        public void SetPartyCam(PartyCam cam)
        {
            _partyCamera = cam;
        }

        public void SetCameraMode(CameraMode mode)
        {
            _cameraMode = mode;
        }

        public CameraMode GetCameraMode()
        {
            return _cameraMode;
        }
    }
}