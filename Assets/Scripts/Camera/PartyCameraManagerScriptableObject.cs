using System;
using UnityEngine;
using System.Collections.Generic;

namespace Manapotion.PartySystem.Cam
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/New PartyCameraManagerScriptableObject")]
    public class PartyCameraManagerScriptableObject : ScriptableObject
    {
        public event Action CameraTargetAdded;

        public List<Transform> targets;

        public Transform AddCameraTarget(Transform target)
        {
            if (targets == null)
            {
                targets = new List<Transform>();
            }

            targets.Add(target);
            CameraTargetAdded.Invoke();
            return target;
        }

        public Transform SetCameraTargetBias(Transform biasedTarget = null, int biasedTargetIndex = 0)
        {
            // first check if the target transform is a part of the target list of if the target index is within the target list bounds
            // then make the camera follow the biased target more strongly than the rest

            throw new NotImplementedException();
        }
    }
}