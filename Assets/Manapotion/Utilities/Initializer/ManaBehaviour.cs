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

        void _SummonObject(GameObject g) {
            var go = Instantiate(g);
        }
        void _SummonObject(GameObject g, Transform p) {
            var go = Instantiate(g, p);
        }
        void _SummonObject(GameObject g, Vector3 t, Quaternion r) {
            var go = Instantiate(g, t, r);
        }
        void _SummonObject(GameObject g, Vector3 t, Quaternion r, Transform p) {
            var go = Instantiate(g, t, r, p);
        }

        public static void DestroyObject(GameObject g, float d) {
            instance._DestroyObject(g, d);
        }

        public static void SummonObject(GameObject g) {
            instance._SummonObject(g);
        }
        public static void SummonObject(GameObject g, Transform p) {
            instance._SummonObject(g, p);
        }
        public static void SummonObject(GameObject g, Vector3 t, Quaternion r) {
            instance._SummonObject(g, t, r);
        }
        public static void SummonObject(GameObject g, Vector3 t, Quaternion r, Transform p) {
            instance._SummonObject(g, t, r, p);
        }
    }
}
