using System;
using UnityEngine;

namespace Manapotion.ManaBehaviour {
    
    public class ManaBehaviour : MonoBehaviour {
        private static ManaBehaviour instance = null;
        
        public static Action OnStart;
        public static Action OnUpdate;
 
        void Awake() {
            if ( instance == null ) {
                instance = this;
            }
        }

        void Start() {
            if ( OnStart != null ) OnStart();
        }
 
        void Update() {
            if ( OnUpdate != null ) OnUpdate();
        }

        void _DestroyObject(GameObject g, float d) {
            Destroy(g, d);
        }

        public static void DestroyObject(GameObject g, float d) {
            instance._DestroyObject(g, d);
        }
    }
}
