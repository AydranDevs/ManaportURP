using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.ManaBehaviour {
    
    public class ManaBehaviour : MonoBehaviour {

        public virtual void InitializePartyMember() {}
        
        public virtual void PathfindingUpdate() {}

        private void Update() {
            
        }
    }
}
