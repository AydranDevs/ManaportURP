using UnityEngine;

namespace Manapotion.AssetManagement
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;

        public static GameAssets i
        {
            get
            {
                if (_i == null)
                {
                    _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                }
                return _i;
            }
        }

        public Transform PFDamageNumber;
    }
}