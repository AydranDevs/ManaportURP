using UnityEngine;

namespace Manapotion.Actions.Targets
{
    public interface ITargetable
    {
        void GetPosition(out Vector2 pos);
        Transform GetTransform();
    }
}