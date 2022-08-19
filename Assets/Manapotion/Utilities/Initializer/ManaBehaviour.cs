using System;
using UnityEngine;

namespace Manapotion
{    
    public class ManaBehaviour : MonoBehaviour
    {
        private static ManaBehaviour instance = null;
        
        public static Action OnStart;
        public static Action OnUpdate;
 
        void Awake()
        {
            if ( instance == null )
            {
                instance = this;
            }
        }

        void Start()
        {
            if ( OnStart != null ) OnStart();
        }
 
        void Update()
        {
            if ( OnUpdate != null ) OnUpdate();
        }
    }
}
