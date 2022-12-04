using UnityEngine;

namespace Manapotion.Actions.Targets
{
    [System.Serializable]
    public struct TargetableData
    {
        public bool targetCandidate;
        public Vector3 currentPosition;
    }

    public interface Targetable
    {
        TargetableData GetTargetableData();
    }
}